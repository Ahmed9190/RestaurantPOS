namespace RestaurantPOS.Entities;

public class Customer
{
  public int Id { get; set; }
  public string Name { get; set; } = default!;
  public string? Phone { get; set; }
  public string? Email { get; set; }

  public List<Order> Orders { get; set; } = new();
}
