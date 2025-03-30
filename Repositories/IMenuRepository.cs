using RestaurantPOS.Entities;

namespace RestaurantPOS.Repositories;

public interface IMenuRepository
{
  Task<MenuItem> CreateAsync(MenuItem item);
  Task<List<MenuItem>> GetAllAsync();
  Task<MenuItem?> GetByIdAsync(int id);
  Task<bool> UpdateAsync(MenuItem item);
  Task<bool> DeleteAsync(int id);
}