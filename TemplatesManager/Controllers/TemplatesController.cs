using UoW;
using Models;
using Dtos;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class TemplatesController : ControllerBase
{
    private readonly TemplatesService templatesService;

    public TemplatesController(TemplatesService _templatesService)
    {
        templatesService = _templatesService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTemplate([FromBody] TemplateDto templateDto)
    {
        var result = await templatesService.Create(templateDto);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        else
            return BadRequest(result.Error);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTemplate([FromBody] TemplateDto templateDto, int id)
    {
        var result = await templatesService.Update(templateDto, id);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        else
            return BadRequest(result.Error);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTemplate( int id)
    {
        var result = await templatesService.Delete(id);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        else
            return BadRequest(result.Error);
    }

    [HttpGet]
    public async Task<ActionResult<List<Template>>> GetAllTemplates()
    {
        var result = await templatesService.GetAll();

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        else
            return NotFound(result.Error);
    }

    [HttpPost("{id}/generate-pdf")]
    public async Task<IActionResult> GeneratePdf(int id, [FromBody] Dictionary<string, string> data)
    {
        var result = await templatesService.GeneratePdfAsync(id, data);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return File(result.Value, "application/pdf", "Document.pdf");
    }
}