using AutoMapper;
using RestaurantPOS.DTOs;
using RestaurantPOS.Entities;
using RestaurantPOS.Repositories;
using RestaurantPOS.Exceptions;

namespace RestaurantPOS.Services;

public class CategoryService
{
  private readonly ICategoryRepository _repo;
  private readonly IMapper _mapper;

  public CategoryService(ICategoryRepository repo, IMapper mapper)
  {
    _repo = repo;
    _mapper = mapper;
  }

  public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
  {
    var entity = _mapper.Map<Category>(dto);
    var saved = await _repo.CreateAsync(entity);
    return _mapper.Map<CategoryDto>(saved);
  }

  public async Task<List<CategoryDto>> GetAllAsync()
  {
    var categories = await _repo.GetAllAsync();
    return _mapper.Map<List<CategoryDto>>(categories);
  }

  public async Task<CategoryDto?> GetByIdAsync(int id)
  {
    var cat = await _repo.GetByIdAsync(id);
    return cat == null ? null : _mapper.Map<CategoryDto>(cat);
  }

  public async Task<bool> UpdateAsync(int id, UpdateCategoryDto dto)
  {
    var existing = await _repo.GetByIdAsync(id);
    if (existing == null)
      throw new ResourceNotFoundException($"Category #{id} not found");

    _mapper.Map(dto, existing);
    return await _repo.UpdateAsync(existing);
  }

  public async Task<bool> DeleteAsync(int id)
  {
    return await _repo.DeleteAsync(id);
  }
}
