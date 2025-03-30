using RestaurantPOS.Enums;

namespace RestaurantPOS.DTOs;

public class PaymentDto
{
  public int Id { get; set; }
  public int OrderId { get; set; }
  public decimal Amount { get; set; }
  public PaymentMethod Method { get; set; }
  public DateTime PaidAt { get; set; }
}
