using Microsoft.AspNetCore.Mvc;
using RestaurantPOS.DTOs;
using RestaurantPOS.Services;

namespace RestaurantPOS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TablesController : ControllerBase
{
  private readonly TableService _service;

  public TablesController(TableService service)
  {
    _service = service;
  }

  [HttpGet]
  public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

  [HttpGet("{id}")]
  public async Task<IActionResult> GetById(int id)
  {
    var result = await _service.GetByIdAsync(id);
    return result == null
        ? NotFound(new { message = $"Table #{id} not found" })
        : Ok(result);
  }

  [HttpPost]
  public async Task<IActionResult> Create(CreateTableDto dto)
  {
    var created = await _service.CreateAsync(dto);
    return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Update(int id, UpdateTableDto dto)
  {
    var updated = await _service.UpdateAsync(id, dto);
    if (!updated) return BadRequest(new { message = $"Table #{id} not found" });
    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var deleted = await _service.DeleteAsync(id);
    if (!deleted) return NotFound(new { message = $"Table #{id} not found" });
    return NoContent();
  }
}
