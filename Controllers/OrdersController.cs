using Microsoft.AspNetCore.Mvc;
using RestaurantPOS.DTOs;
using RestaurantPOS.Services;

namespace RestaurantPOS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
  private readonly OrderService _service;

  public OrdersController(OrderService service)
  {
    _service = service;
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
  {
    var order = await _service.CreateAsync(dto);
    return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
  }

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var orders = await _service.GetAllAsync();
    return Ok(orders);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetById(int id)
  {
    var order = await _service.GetByIdAsync(id);
    if (order == null) return NotFound();
    return Ok(order);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var deleted = await _service.DeleteAsync(id);
    if (!deleted) return NotFound();
    return NoContent();
  }
}