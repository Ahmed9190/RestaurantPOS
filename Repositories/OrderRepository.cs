using Microsoft.EntityFrameworkCore;
using RestaurantPOS.Data;
using RestaurantPOS.Entities;

namespace RestaurantPOS.Repositories;

public class OrderRepository : IOrderRepository
{
  private readonly AppDbContext _context;

  public OrderRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<Order> CreateAsync(Order order)
  {
    _context.Orders.Add(order);
    await _context.SaveChangesAsync();
    return order;
  }

  public async Task<Order?> GetByIdAsync(int id)
  {
    return await _context.Orders.Include(o => o.Items).ThenInclude(i => i.MenuItem)
    .FirstOrDefaultAsync(o => o.Id == id);
  }

  public async Task<List<Order>> GetAllAsync()
  {
    return await _context.Orders
    .Include(o => o.Items)
    .ThenInclude(i => i.MenuItem)
    .ToListAsync();
  }

  public async Task<bool> DeleteAsync(int id)
  {
    var order = await _context.Orders.FindAsync(id);
    if (order == null) return false;

    _context.Orders.Remove(order);
    await _context.SaveChangesAsync();
    return true;
  }
}