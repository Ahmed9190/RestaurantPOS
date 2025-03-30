using AutoMapper;
using RestaurantPOS.DTOs;
using RestaurantPOS.Entities;
using RestaurantPOS.Repositories;
using RestaurantPOS.Exceptions;

namespace RestaurantPOS.Services;

public class ReservationService
{
  private readonly IReservationRepository _repo;
  private readonly ICustomerRepository _customerRepo;
  private readonly ITableRepository _tableRepo;
  private readonly IMapper _mapper;

  public ReservationService(
      IReservationRepository repo,
      ICustomerRepository customerRepo,
      ITableRepository tableRepo,
      IMapper mapper)
  {
    _repo = repo;
    _customerRepo = customerRepo;
    _tableRepo = tableRepo;
    _mapper = mapper;
  }

  public async Task<ReservationDto> CreateAsync(CreateReservationDto dto)
  {
    // Validate customer
    var customerExists = await _customerRepo.ExistsAsync(dto.CustomerId);
    if (!customerExists)
      throw new ResourceNotFoundException($"Customer #{dto.CustomerId} not found");

    // Validate table
    var table = await _tableRepo.GetByIdAsync(dto.TableId);
    if (table == null)
      throw new ResourceNotFoundException($"Table #{dto.TableId} not found");

    if (await _repo.IsTableReservedAsync(dto.TableId, dto.ReservedAt))
      throw new ValidationException($"Table #{dto.TableId} is already reserved at {dto.ReservedAt}");

    // Map DTO to entity and save
    var reservation = _mapper.Map<Reservation>(dto);
    var saved = await _repo.CreateAsync(reservation);

    return _mapper.Map<ReservationDto>(saved);
  }

  public async Task<List<ReservationDto>> GetAllAsync()
  {
    var list = await _repo.GetAllAsync();
    return _mapper.Map<List<ReservationDto>>(list);
  }

  public async Task<ReservationDto?> GetByIdAsync(int id)
  {
    var r = await _repo.GetByIdAsync(id);
    return r == null ? null : _mapper.Map<ReservationDto>(r);
  }

  public async Task<bool> DeleteAsync(int id)
  {
    return await _repo.DeleteAsync(id);
  }
}
