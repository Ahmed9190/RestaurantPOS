using Microsoft.EntityFrameworkCore;
using RestaurantPOS.Data;
using RestaurantPOS.Entities;

namespace RestaurantPOS.Repositories;

public class CustomerRepository : ICustomerRepository
{
  private readonly AppDbContext _context;

  public CustomerRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<Customer> CreateAsync(Customer customer)
  {
    _context.Customers.Add(customer);
    await _context.SaveChangesAsync();
    return customer;
  }

  public async Task<List<Customer>> GetAllAsync()
  {
    return await _context.Customers.ToListAsync();
  }

  public async Task<Customer?> GetByIdAsync(int id)
  {
    return await _context.Customers.FindAsync(id);
  }

  public async Task<bool> UpdateAsync(Customer customer)
  {
    var existing = await _context.Customers.FindAsync(customer.Id);
    if (existing == null) return false;

    existing.Name = customer.Name;
    existing.Phone = customer.Phone;
    existing.Email = customer.Email;

    await _context.SaveChangesAsync();
    return true;
  }

  public async Task<bool> DeleteAsync(int id)
  {
    var customer = await _context.Customers.FindAsync(id);
    if (customer == null) return false;

    _context.Customers.Remove(customer);
    await _context.SaveChangesAsync();
    return true;
  }

  public async Task<bool> ExistsAsync(int id)
  {
    return await _context.Customers.AnyAsync(c => c.Id == id);
  }
}
