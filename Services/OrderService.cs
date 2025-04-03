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
    Table? table = null;

    if (dto.TableId.HasValue)
    {
      // Validate table only if provided
      table = await _tableRepo.GetByIdAsync(dto.TableId.Value)
          ?? throw new ResourceNotFoundException($"Table #{dto.TableId} not found");

      if (table.Status == TableStatus.OutOfService)
        throw new ValidationException($"Table #{table.Number} is out of service");

      if (table.Status == TableStatus.Occupied)
        throw new ValidationException($"Table #{table.Number} is already occupied");
    }

    // Validate customer (if provided)
    if (dto.CustomerId.HasValue && !await _customerRepo.ExistsAsync(dto.CustomerId.Value))
      throw new ResourceNotFoundException($"Customer #{dto.CustomerId} not found");

    // Build order
    var order = new Order
    {
      TableId = dto.TableId,
      CreatedAt = DateTime.UtcNow,
      CustomerId = dto.CustomerId
    };

    // Calculate total and add items
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

    // Mark table as occupied (only if it exists)
    if (table != null)
    {
      table.Status = TableStatus.Occupied;
      await _tableRepo.UpdateAsync(table);
    }

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
