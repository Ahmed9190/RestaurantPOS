namespace RestaurantPOS.Entities;

public class Category
{
  public int Id { get; set; }
  public string Name { get; set; } = default!;

  public List<MenuItem> MenuItems { get; set; } = new();
}
