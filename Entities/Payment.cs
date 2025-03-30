using RestaurantPOS.Enums;

namespace RestaurantPOS.Entities;

public class Payment
{
  public int Id { get; set; }
  public int OrderId { get; set; }
  public Order Order { get; set; } = default!;
  public decimal Amount { get; set; }
  public PaymentMethod Method { get; set; }
  public DateTime PaidAt { get; set; } = DateTime.UtcNow;
}
