using RestaurantPOS.Enums;

namespace RestaurantPOS.DTOs;

public class CreateMenuItemDto
{
  public string Name { get; set; } = default!;
  public decimal Price { get; set; }
  public int CategoryId { get; set; }
}