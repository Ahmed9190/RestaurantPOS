using AutoMapper;
using RestaurantPOS.DTOs;
using RestaurantPOS.Entities;
using RestaurantPOS.Repositories;
using RestaurantPOS.Exceptions;

namespace RestaurantPOS.Services;

public class CustomerService
{
  private readonly ICustomerRepository _repo;
  private readonly IMapper _mapper;

  public CustomerService(ICustomerRepository repo, IMapper mapper)
  {
    _repo = repo;
    _mapper = mapper;
  }

  public async Task<CustomerDto> CreateAsync(CreateCustomerDto dto)
  {
    var entity = _mapper.Map<Customer>(dto);
    var saved = await _repo.CreateAsync(entity);
    return _mapper.Map<CustomerDto>(saved);
  }

  public async Task<List<CustomerDto>> GetAllAsync()
  {
    var list = await _repo.GetAllAsync();
    return _mapper.Map<List<CustomerDto>>(list);
  }

  public async Task<CustomerDto?> GetByIdAsync(int id)
  {
    var c = await _repo.GetByIdAsync(id);
    return c == null ? null : _mapper.Map<CustomerDto>(c);
  }

  public async Task<bool> UpdateAsync(int id, UpdateCustomerDto dto)
  {
    var existing = await _repo.GetByIdAsync(id);
    if (existing == null)
      throw new ResourceNotFoundException($"Customer #{id} not found");

    _mapper.Map(dto, existing);
    return await _repo.UpdateAsync(existing);
  }

  public async Task<bool> DeleteAsync(int id)
  {
    return await _repo.DeleteAsync(id);
  }
}
