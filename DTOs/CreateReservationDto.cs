namespace RestaurantPOS.DTOs;

public class CreateReservationDto
{
  public int TableId { get; set; }
  public int CustomerId { get; set; }
  public DateTime ReservedAt { get; set; }
  public string? Note { get; set; }
}
