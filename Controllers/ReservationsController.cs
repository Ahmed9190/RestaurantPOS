using Microsoft.AspNetCore.Mvc;
using RestaurantPOS.DTOs;
using RestaurantPOS.Services;

namespace RestaurantPOS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
  private readonly ReservationService _service;

  public ReservationsController(ReservationService service)
  {
    _service = service;
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateReservationDto dto)
  {
    try
    {
      var created = await _service.CreateAsync(dto);
      return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    catch (Exception ex)
    {
      return BadRequest(new { message = ex.Message });
    }
  }

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    return Ok(await _service.GetAllAsync());
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetById(int id)
  {
    var result = await _service.GetByIdAsync(id);
    return result == null
        ? NotFound(new { message = $"Reservation #{id} not found" })
        : Ok(result);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var deleted = await _service.DeleteAsync(id);
    if (!deleted)
      return NotFound(new { message = $"Reservation #{id} not found" });
    return NoContent();
  }
}
