namespace RestaurantPOS.DTOs;

public class UpdateCustomerDto
{
  public string Name { get; set; } = default!;
  public string? Phone { get; set; }
  public string? Email { get; set; }
}
