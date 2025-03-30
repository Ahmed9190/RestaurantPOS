namespace RestaurantPOS.DTOs;

public class ReservationDto
{
  public int Id { get; set; }
  public int TableId { get; set; }
  public int CustomerId { get; set; }
  public DateTime ReservedAt { get; set; }
  public string? Note { get; set; }
}
