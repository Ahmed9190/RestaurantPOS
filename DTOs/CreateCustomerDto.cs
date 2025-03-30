namespace RestaurantPOS.DTOs;

public class CreateCustomerDto
{
  public string Name { get; set; } = default!;
  public string? Phone { get; set; }
  public string? Email { get; set; }
}
