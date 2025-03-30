using AutoMapper;
using RestaurantPOS.DTOs;
using RestaurantPOS.Entities;
using RestaurantPOS.Repositories;
using RestaurantPOS.Exceptions;

namespace RestaurantPOS.Services;

public class TableService
{
  private readonly ITableRepository _repo;
  private readonly IMapper _mapper;

  public TableService(ITableRepository repo, IMapper mapper)
  {
    _repo = repo;
    _mapper = mapper;
  }

  public async Task<TableDto> CreateAsync(CreateTableDto dto)
  {
    var entity = _mapper.Map<Table>(dto);
    var saved = await _repo.CreateAsync(entity);
    return _mapper.Map<TableDto>(saved);
  }

  public async Task<List<TableDto>> GetAllAsync()
  {
    var list = await _repo.GetAllAsync();
    return _mapper.Map<List<TableDto>>(list);
  }

  public async Task<TableDto?> GetByIdAsync(int id)
  {
    var t = await _repo.GetByIdAsync(id);
    return t == null ? null : _mapper.Map<TableDto>(t);
  }

  public async Task<bool> UpdateAsync(int id, UpdateTableDto dto)
  {
    var existing = await _repo.GetByIdAsync(id);
    if (existing == null)
      throw new ResourceNotFoundException($"Table #{id} not found");

    _mapper.Map(dto, existing);
    return await _repo.UpdateAsync(existing);
  }

  public async Task<bool> DeleteAsync(int id)
  {
    return await _repo.DeleteAsync(id);
  }
}
