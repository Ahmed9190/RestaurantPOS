using RestaurantPOS.Entities;

namespace RestaurantPOS.Repositories;

public interface ITableRepository
{
  Task<Table> CreateAsync(Table table);
  Task<List<Table>> GetAllAsync();
  Task<Table?> GetByIdAsync(int id);
  Task<bool> UpdateAsync(Table table);
  Task<bool> DeleteAsync(int id);

  Task<Table?> GetByNumberAsync(int number);
}
