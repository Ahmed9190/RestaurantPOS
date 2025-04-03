namespace RestaurantPOS.DTOs;

public class OrderDto
{
  public int Id { get; set; }
  public int? TableId { get; set; }
  public int? TableNumber { get; set; } // mapped from Table.Number
  public DateTime CreatedAt { get; set; }
  public decimal TotalPrice { get; set; }
}