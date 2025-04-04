using RestaurantPOS.Enums;

namespace RestaurantPOS.Entities;

public class MenuItem
{
  public int Id { get; set; }
  public string Name { get; set; } = default!;
  public decimal Price { get; set; }
  public int CategoryId { get; set; }
  public Category Category { get; set; } = default!;
}
