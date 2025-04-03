namespace RestaurantPOS.DTOs;

public class CreateOrderDto
{
    public int? TableId { get; set; }
    public List<CreateOrderItemDto> Items { get; set; } = new();
    public int? CustomerId { get; set; }
}