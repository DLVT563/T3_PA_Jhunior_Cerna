using System.ComponentModel.DataAnnotations;

namespace TechSolutionsApp.Models;

public class Project
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    [Display(Name = "Descripción")]
    public string? Description { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Fecha de inicio")]
    public DateTime StartDate { get; set; }

    [Display(Name = "Usuario responsable")]
    public int UserId { get; set; }

    public User? User { get; set; }

    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
