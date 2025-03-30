namespace RestaurantPOS.Entities;

public class Reservation
{
  public int Id { get; set; }

  public int TableId { get; set; }
  public Table Table { get; set; } = default!;

  public int CustomerId { get; set; }
  public Customer Customer { get; set; } = default!;

  public DateTime ReservedAt { get; set; }
  public string Note { get; set; } = string.Empty;
}
