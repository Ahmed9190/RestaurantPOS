using Microsoft.EntityFrameworkCore;
using RestaurantPOS.Data;
using RestaurantPOS.Entities;

namespace RestaurantPOS.Repositories;

public class PaymentRepository : IPaymentRepository
{
  private readonly AppDbContext _context;

  public PaymentRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<Payment> CreateAsync(Payment payment)
  {
    _context.Payments.Add(payment);
    await _context.SaveChangesAsync();
    return payment;
  }

  public async Task<List<Payment>> GetAllAsync()
  {
    return await _context.Payments.Include(p => p.Order).ToListAsync();
  }

  public async Task<Payment?> GetByIdAsync(int id)
  {
    return await _context.Payments.Include(p => p.Order).FirstOrDefaultAsync(p => p.Id == id);
  }
}
