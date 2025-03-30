using RestaurantPOS.Entities;

namespace RestaurantPOS.Repositories;

public interface ICategoryRepository
{
  Task<Category> CreateAsync(Category category);
  Task<List<Category>> GetAllAsync();
  Task<Category?> GetByIdAsync(int id);
  Task<bool> UpdateAsync(Category category);
  Task<bool> DeleteAsync(int id);
}
