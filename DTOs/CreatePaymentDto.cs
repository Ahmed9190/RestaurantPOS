using RestaurantPOS.Enums;

namespace RestaurantPOS.DTOs;

public class CreatePaymentDto
{
  public int OrderId { get; set; }
  public decimal Amount { get; set; }
  public PaymentMethod Method { get; set; }
}
