using Microsoft.EntityFrameworkCore;
using RestaurantPOS.Data;
using RestaurantPOS.Entities;

namespace RestaurantPOS.Repositories;

public class CategoryRepository : ICategoryRepository
{
  private readonly AppDbContext _context;

  public CategoryRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<Category> CreateAsync(Category category)
  {
    _context.Categories.Add(category);
    await _context.SaveChangesAsync();
    return category;
  }

  public async Task<List<Category>> GetAllAsync()
  {
    return await _context.Categories.ToListAsync();
  }

  public async Task<Category?> GetByIdAsync(int id)
  {
    return await _context.Categories.FindAsync(id);
  }

  public async Task<bool> UpdateAsync(Category category)
  {
    var existing = await _context.Categories.FindAsync(category.Id);
    if (existing == null) return false;

    existing.Name = category.Name;
    await _context.SaveChangesAsync();
    return true;
  }

  public async Task<bool> DeleteAsync(int id)
  {
    var category = await _context.Categories.FindAsync(id);
    if (category == null) return false;

    _context.Categories.Remove(category);
    await _context.SaveChangesAsync();
    return true;
  }
}
