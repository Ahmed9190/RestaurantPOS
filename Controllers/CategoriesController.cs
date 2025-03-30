using Microsoft.AspNetCore.Mvc;
using RestaurantPOS.DTOs;
using RestaurantPOS.Services;

namespace RestaurantPOS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
  private readonly CategoryService _service;

  public CategoriesController(CategoryService service)
  {
    _service = service;
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
    var cat = await _service.GetByIdAsync(id);
    if (cat == null) return NotFound(new { message = $"Category #{id} not found" });
    return Ok(cat);
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
  {
    var created = await _service.CreateAsync(dto);
    return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto dto)
  {
    try
    {
      var updated = await _service.UpdateAsync(id, dto);
      if (!updated) return NotFound(new { message = $"Category #{id} not found" });
      return NoContent();
    }
    catch (Exception ex)
    {
      return BadRequest(new { message = ex.Message });
    }
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var deleted = await _service.DeleteAsync(id);
    if (!deleted) return NotFound(new { message = $"Category #{id} not found" });
    return NoContent();
  }
}
