using RestaurantPOS.Data;
using RestaurantPOS.Entities;

namespace RestaurantPOS.Repositories;

public interface IOrderRepository
{

  Task<Order> CreateAsync(Order order);
  Task<Order?> GetByIdAsync(int id);
  Task<List<Order>> GetAllAsync();
  Task<bool> DeleteAsync(int id);
}