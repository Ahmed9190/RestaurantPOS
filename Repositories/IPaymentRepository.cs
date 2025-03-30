using RestaurantPOS.Entities;

namespace RestaurantPOS.Repositories;

public interface IPaymentRepository
{
  Task<Payment> CreateAsync(Payment payment);
  Task<List<Payment>> GetAllAsync();
  Task<Payment?> GetByIdAsync(int id);
}
