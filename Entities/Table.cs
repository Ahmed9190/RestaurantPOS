using RestaurantPOS.Enums;

public class Table
{
  public int Id { get; set; }
  public int Number { get; set; }
  public int Capacity { get; set; }
  public TableStatus Status { get; set; } = TableStatus.Available;
}
