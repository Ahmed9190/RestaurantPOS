using RestaurantPOS.Entities;
using RestaurantPOS.Enums;

namespace RestaurantPOS.DTOs;

public class MenuItemDto
{
  public int Id { get; set; }
  public string Name { get; set; } = default!;
  public decimal Price { get; set; }
  public int CategoryId { get; set; }
  public Category Category { get; set; } = default!;
}
