namespace RestaurantPOS.Entities;

public class Order
{
    public int Id { get; set; }
    public int? TableId { get; set; }
    public Table? Table { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalPrice { get; set; }

    public List<OrderItem> Items { get; set; } = new();
    public int? CustomerId { get; set; }   // Nullable for walk-ins
    public Customer? Customer { get; set; }
}