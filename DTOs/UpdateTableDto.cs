using RestaurantPOS.Enums;

public class UpdateTableDto
{
  public int Capacity { get; set; }
  public TableStatus Status { get; set; } = TableStatus.Available;
}
