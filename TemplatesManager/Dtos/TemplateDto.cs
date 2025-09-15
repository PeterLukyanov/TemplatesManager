using Models;
using System.ComponentModel.DataAnnotations;
namespace Dtos;

public class TemplateDto
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Content { get; set; } = null!;
}