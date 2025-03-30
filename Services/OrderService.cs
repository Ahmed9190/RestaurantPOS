using AutoMapper;
using RestaurantPOS.DTOs;
using RestaurantPOS.Enums;
using RestaurantPOS.Entities;
using RestaurantPOS.Repositories;
using RestaurantPOS.Exceptions;

namespace RestaurantPOS.Services;

public class OrderService
{
  private readonly IOrderRepository _orderRepo;
  private readonly IMenuRepository _menuRepo;
  private readonly ICustomerRepository _customerRepo;
  private readonly ITableRepository _tableRepo;
  private readonly IMapper _mapper;

  public OrderService(
      IOrderRepository orderRepo,
      IMenuRepository menuRepo,
      ICustomerRepository customerRepo,
      ITableRepository tableRepo,
      IMapper mapper)
  {
    _orderRepo = orderRepo;
    _menuRepo = menuRepo;
    _customerRepo = customerRepo;
    _tableRepo = tableRepo;
    _mapper = mapper;
  }

  public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
  {
    // Validate table using dedicated method
    var table = await _tableRepo.GetByNumberAsync(dto.TableNumber)
        ?? throw new ResourceNotFoundException($"Table #{dto.TableNumber} not found");

    if (table.Status == TableStatus.OutOfService)
      throw new ValidationException($"Table #{dto.TableNumber} is out of service");

    if (table.Status == TableStatus.Occupied)
      throw new ValidationException($"Table #{dto.TableNumber} is already occupied");

    // Validate customer if provided
    if (dto.CustomerId.HasValue)
    {
      var exists = await _customerRepo.ExistsAsync(dto.CustomerId.Value);
      if (!exists)
        throw new ResourceNotFoundException($"Customer #{dto.CustomerId.Value} not found");
    }

    // Build Order with custom logic
    var order = new Order
    {
      TableNumber = dto.TableNumber,
      CreatedAt = DateTime.UtcNow,
      CustomerId = dto.CustomerId
    };

    decimal total = 0;
    foreach (var itemDto in dto.Items)
    {
      var menuItem = await _menuRepo.GetByIdAsync(itemDto.MenuItemId)
          ?? throw new ResourceNotFoundException($"MenuItem #{itemDto.MenuItemId} not found");

      total += menuItem.Price * itemDto.Quantity;
      order.Items.Add(new OrderItem
      {
        MenuItemId = menuItem.Id,
        Quantity = itemDto.Quantity,
        Price = menuItem.Price
      });
    }
    order.TotalPrice = total;

    // Save order
    var saved = await _orderRepo.CreateAsync(order);

    // Mark table as occupied
    table.Status = TableStatus.Occupied;
    await _tableRepo.UpdateAsync(table);

    // Use AutoMapper to return the DTO
    return _mapper.Map<OrderDto>(saved);
  }

  public async Task<List<OrderDto>> GetAllAsync()
  {
    var orders = await _orderRepo.GetAllAsync();
    return _mapper.Map<List<OrderDto>>(orders);
  }

  public async Task<OrderDto?> GetByIdAsync(int id)
  {
    var o = await _orderRepo.GetByIdAsync(id);
    return o == null ? null : _mapper.Map<OrderDto>(o);
  }

  public async Task<bool> DeleteAsync(int id)
  {
    return await _orderRepo.DeleteAsync(id);
  }
}
