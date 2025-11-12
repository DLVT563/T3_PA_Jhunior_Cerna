using Microsoft.EntityFrameworkCore;
using TechSolutionsApp.Models;
using TechSolutionsApp.Utilities;

namespace TechSolutionsApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>().ToTable("Roles");
        modelBuilder.Entity<User>().ToTable("Usuarios");
        modelBuilder.Entity<Project>().ToTable("Proyectos");
        modelBuilder.Entity<TaskItem>().ToTable("Tareas");

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Project>()
            .HasOne(p => p.User)
            .WithMany(u => u.Projects)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        var adminRoleId = 1;
        var userRoleId = 2;
        var clientRoleId = 3;

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = adminRoleId, Name = "Admin" },
            new Role { Id = userRoleId, Name = "Usuario" },
            new Role { Id = clientRoleId, Name = "Cliente" }
        );

        var adminUserId = 1;
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = adminUserId,
                Name = "Administrador",
                Email = "admin@techsolutions.com",
                PasswordHash = PasswordHasher.Hash("Admin123!"),
                RoleId = adminRoleId
            }
        );
    }
}
