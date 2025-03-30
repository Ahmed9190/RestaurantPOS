namespace RestaurantPOS.DTOs;

public class CustomerDto
{
  public int Id { get; set; }
  public string Name { get; set; } = default!;
  public string? Phone { get; set; }
  public string? Email { get; set; }
}
