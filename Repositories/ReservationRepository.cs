using Microsoft.EntityFrameworkCore;
using RestaurantPOS.Data;
using RestaurantPOS.Entities;

namespace RestaurantPOS.Repositories;

public class ReservationRepository : IReservationRepository
{
  private readonly AppDbContext _context;

  public ReservationRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<Reservation> CreateAsync(Reservation reservation)
  {
    _context.Reservations.Add(reservation);
    await _context.SaveChangesAsync();
    return reservation;
  }

  public async Task<List<Reservation>> GetAllAsync()
  {
    return await _context.Reservations
        .Include(r => r.Table)
        .Include(r => r.Customer)
        .ToListAsync();
  }

  public async Task<Reservation?> GetByIdAsync(int id)
  {
    return await _context.Reservations
        .Include(r => r.Table)
        .Include(r => r.Customer)
        .FirstOrDefaultAsync(r => r.Id == id);
  }

  public async Task<bool> DeleteAsync(int id)
  {
    var reservation = await _context.Reservations.FindAsync(id);
    if (reservation == null) return false;

    _context.Reservations.Remove(reservation);
    await _context.SaveChangesAsync();
    return true;
  }

  public async Task<bool> IsTableReservedAsync(int tableId, DateTime reservedAt)
  {
    return await _context.Reservations.AnyAsync(r =>
        r.TableId == tableId &&
        r.ReservedAt == reservedAt);
  }
}
