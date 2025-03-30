namespace RestaurantPOS.DTOs;

public class OrderDto
{
  public int Id { get; set; }
  public int TableNumber { get; set; }
  public DateTime CreatedAt { get; set; }
  public decimal TotalPrice { get; set; }
}