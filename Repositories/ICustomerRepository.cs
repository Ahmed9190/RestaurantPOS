using RestaurantPOS.Entities;

namespace RestaurantPOS.Repositories;

public interface ICustomerRepository
{
  Task<Customer> CreateAsync(Customer customer);
  Task<List<Customer>> GetAllAsync();
  Task<Customer?> GetByIdAsync(int id);
  Task<bool> UpdateAsync(Customer customer);
  Task<bool> DeleteAsync(int id);

  Task<bool> ExistsAsync(int id);
}
