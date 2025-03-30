using Microsoft.EntityFrameworkCore;
using RestaurantPOS.Data;
using RestaurantPOS.Entities;

namespace RestaurantPOS.Repositories;

public class TableRepository : ITableRepository
{
  private readonly AppDbContext _context;

  public TableRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<Table> CreateAsync(Table table)
  {
    _context.Tables.Add(table);
    await _context.SaveChangesAsync();
    return table;
  }

  public async Task<List<Table>> GetAllAsync()
  {
    return await _context.Tables.ToListAsync();
  }

  public async Task<Table?> GetByIdAsync(int id)
  {
    return await _context.Tables.FindAsync(id);
  }

  public async Task<bool> UpdateAsync(Table table)
  {
    var existing = await _context.Tables.FindAsync(table.Id);
    if (existing == null) return false;

    existing.Capacity = table.Capacity;
    existing.Status = table.Status;
    await _context.SaveChangesAsync();
    return true;
  }

  public async Task<bool> DeleteAsync(int id)
  {
    var table = await _context.Tables.FindAsync(id);
    if (table == null) return false;

    _context.Tables.Remove(table);
    await _context.SaveChangesAsync();
    return true;
  }

  public async Task<Table?> GetByNumberAsync(int number)
  {
    return await _context.Tables
        .FirstOrDefaultAsync(t => t.Number == number);
  }
}
