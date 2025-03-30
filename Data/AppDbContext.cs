using Microsoft.EntityFrameworkCore;
using RestaurantPOS.Entities;

namespace RestaurantPOS.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {

  }

  public DbSet<MenuItem> MenuItems => Set<MenuItem>();
  public DbSet<Order> Orders => Set<Order>();
  public DbSet<OrderItem> OrderItems => Set<OrderItem>();
  public DbSet<Payment> Payments => Set<Payment>();
  public DbSet<Customer> Customers => Set<Customer>();
  public DbSet<Table> Tables => Set<Table>();
  public DbSet<Reservation> Reservations => Set<Reservation>();
  public DbSet<Category> Categories => Set<Category>();
}


