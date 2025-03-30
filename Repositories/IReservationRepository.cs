using RestaurantPOS.Entities;

namespace RestaurantPOS.Repositories;

public interface IReservationRepository
{
  Task<Reservation> CreateAsync(Reservation reservation);
  Task<List<Reservation>> GetAllAsync();
  Task<Reservation?> GetByIdAsync(int id);
  Task<bool> DeleteAsync(int id);
  Task<bool> IsTableReservedAsync(int tableId, DateTime reservedAt);
}
