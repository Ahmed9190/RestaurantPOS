using Microsoft.AspNetCore.Mvc;
using RestaurantPOS.DTOs;
using RestaurantPOS.Services;

namespace RestaurantPOS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
  private readonly MenuService _service;

  public MenuController(MenuService service)
  {
    _service = service;
  }



  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateMenuItemDto dto)
  {
    var created = await _service.CreateAsync(dto);
    return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
  }

  [HttpGet]
  public async Task<IActionResult> GetMenu()
  {
    var result = await _service.GetMenuAsync();
    return Ok(result);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> Get(int id)
  {
    var item = await _service.GetByIdAsync(id);
    if (item == null) return NotFound();
    return Ok(item);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Update(int id, [FromBody] UpdateMenuItemDto dto)
  {
    var updated = await _service.UpdateAsync(id, dto);
    if (!updated) return NotFound();
    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var deleted = await _service.DeleteAsync(id);
    if (!deleted) return NotFound();
    return NoContent();
  }
}