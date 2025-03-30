using AutoMapper;
using RestaurantPOS.DTOs;
using RestaurantPOS.Entities;
using RestaurantPOS.Repositories;
using RestaurantPOS.Exceptions;

namespace RestaurantPOS.Services;

public class MenuService
{
  private readonly IMenuRepository _repository;
  private readonly ICategoryRepository _categoryRepo;
  private readonly IMapper _mapper;

  public MenuService(
    IMenuRepository repository,
    ICategoryRepository categoryRepo,
    IMapper mapper
  )
  {
    _repository = repository;
    _categoryRepo = categoryRepo;
    _mapper = mapper;
  }


  public async Task<MenuItemDto> CreateAsync(CreateMenuItemDto dto)
  {
    // validate category
    var category = await _categoryRepo.GetByIdAsync(dto.CategoryId);
    if (category == null)
      throw new ResourceNotFoundException($"Category #{dto.CategoryId} not found");

    var entity = _mapper.Map<MenuItem>(dto);
    entity.CategoryId = category.Id;

    var result = await _repository.CreateAsync(entity);
    return _mapper.Map<MenuItemDto>(result);
  }


  public async Task<List<MenuItemDto>> GetMenuAsync()
  {
    var items = await _repository.GetAllAsync();
    return _mapper.Map<List<MenuItemDto>>(items);
  }

  public async Task<MenuItemDto?> GetByIdAsync(int id)
  {
    var item = await _repository.GetByIdAsync(id);
    return item == null ? null : _mapper.Map<MenuItemDto>(item);
  }

  public async Task<bool> UpdateAsync(int id, UpdateMenuItemDto dto)
  {
    var existing = await _repository.GetByIdAsync(id);
    if (existing == null)
      throw new ResourceNotFoundException($"MenuItem #{id} not found");

    var category = await _categoryRepo.GetByIdAsync(dto.CategoryId);
    if (category == null)
      throw new ResourceNotFoundException($"Category #{dto.CategoryId} not found");

    _mapper.Map(dto, existing);
    existing.CategoryId = category.Id;

    return await _repository.UpdateAsync(existing);
  }

  public async Task<bool> DeleteAsync(int id)
  {
    return await _repository.DeleteAsync(id);
  }
}
