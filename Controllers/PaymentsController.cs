using Microsoft.AspNetCore.Mvc;
using RestaurantPOS.DTOs;
using RestaurantPOS.Services;

namespace RestaurantPOS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
  private readonly PaymentService _service;

  public PaymentsController(PaymentService service)
  {
    _service = service;
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreatePaymentDto dto)
  {
    var payment = await _service.CreateAsync(dto);
    return CreatedAtAction(nameof(GetById), new { id = payment.Id }, payment);
  }

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var result = await _service.GetAllAsync();
    return Ok(result);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetById(int id)
  {
    var result = await _service.GetByIdAsync(id);
    if (result == null) return NotFound();
    return Ok(result);
  }
}
