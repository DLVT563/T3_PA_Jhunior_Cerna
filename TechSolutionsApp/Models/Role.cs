using System.ComponentModel.DataAnnotations;

namespace TechSolutionsApp.Models;

public class Role
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    public ICollection<User> Users { get; set; } = new List<User>();
}
