using System.ComponentModel.DataAnnotations;

namespace TechSolutionsApp.Models;

public class TaskItem
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    [Display(Name = "Título")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Status { get; set; } = string.Empty;

    [Display(Name = "Proyecto")]
    public int ProjectId { get; set; }

    public Project? Project { get; set; }
}
