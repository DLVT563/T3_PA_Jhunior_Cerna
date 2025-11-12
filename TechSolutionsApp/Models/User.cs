using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechSolutionsApp.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string PasswordHash { get; set; } = string.Empty;

    [Display(Name = "Rol")]
    public int RoleId { get; set; }

    public Role? Role { get; set; }

    public ICollection<Project> Projects { get; set; } = new List<Project>();

    [NotMapped]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    public string? Password { get; set; }
}
