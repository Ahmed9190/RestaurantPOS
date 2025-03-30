using Microsoft.EntityFrameworkCore;
using RestaurantPOS.Data;
using RestaurantPOS.Entities;

namespace RestaurantPOS.Repositories;

public class MenuRepository : IMenuRepository
{
  private readonly AppDbContext _context;


  public MenuRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<MenuItem> CreateAsync(MenuItem item)
  {
    _context.MenuItems.Add(item);
    await _context.SaveChangesAsync();
    return item;
  }

  public async Task<List<MenuItem>> GetAllAsync()
  {
    return await _context.MenuItems.ToListAsync();
  }

  public async Task<MenuItem?> GetByIdAsync(int id)
  {
    return await _context.MenuItems.FindAsync(id);
  }

  public async Task<bool> UpdateAsync(MenuItem item)
  {
    var existing = await _context.MenuItems.FindAsync(item.Id);
    if (existing == null) return false;

    existing.Name = item.Name;
    existing.Price = item.Price;
    existing.Category = item.Category;

    await _context.SaveChangesAsync();
    return true;
  }

  public async Task<bool> DeleteAsync(int id)
  {
    var item = await _context.MenuItems.FindAsync(id);
    if (item == null) return false;

    _context.MenuItems.Remove(item);
    await _context.SaveChangesAsync();
    return true;
  }
}