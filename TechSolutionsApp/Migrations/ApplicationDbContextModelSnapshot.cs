using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechSolutionsApp.Data;

#nullable disable

namespace TechSolutionsApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.5");

            modelBuilder.Entity("TechSolutionsApp.Models.Project", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<string>("Description")
                    .HasMaxLength(500)
                    .HasColumnType("TEXT");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("TEXT");

                b.Property<DateTime>("StartDate")
                    .HasColumnType("TEXT");

                b.Property<int>("UserId")
                    .HasColumnType("INTEGER");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("Proyectos");
            });

            modelBuilder.Entity("TechSolutionsApp.Models.Role", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("TEXT");

                b.HasKey("Id");

                b.ToTable("Roles");

                b.HasData(
                    new
                    {
                        Id = 1,
                        Name = "Admin"
                    },
                    new
                    {
                        Id = 2,
                        Name = "Usuario"
                    },
                    new
                    {
                        Id = 3,
                        Name = "Cliente"
                    });
            });

            modelBuilder.Entity("TechSolutionsApp.Models.TaskItem", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<int>("ProjectId")
                    .HasColumnType("INTEGER");

                b.Property<string>("Status")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("TEXT");

                b.Property<string>("Title")
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnType("TEXT");

                b.HasKey("Id");

                b.HasIndex("ProjectId");

                b.ToTable("Tareas");
            });

            modelBuilder.Entity("TechSolutionsApp.Models.User", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnType("TEXT");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("TEXT");

                b.Property<string>("PasswordHash")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("TEXT");

                b.Property<int>("RoleId")
                    .HasColumnType("INTEGER");

                b.HasKey("Id");

                b.HasIndex("Email")
                    .IsUnique();

                b.HasIndex("RoleId");

                b.ToTable("Usuarios");

                b.HasData(
                    new
                    {
                        Id = 1,
                        Email = "admin@techsolutions.com",
                        Name = "Administrador",
                        PasswordHash = "3EB3FE66B31E3B4D10FA70B5CAD49C7112294AF6AE4E476A1C405155D45AA121",
                        RoleId = 1
                    });
            });

            modelBuilder.Entity("TechSolutionsApp.Models.Project", b =>
            {
                b.HasOne("TechSolutionsApp.Models.User", "User")
                    .WithMany("Projects")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("User");
            });

            modelBuilder.Entity("TechSolutionsApp.Models.TaskItem", b =>
            {
                b.HasOne("TechSolutionsApp.Models.Project", "Project")
                    .WithMany("Tasks")
                    .HasForeignKey("ProjectId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Project");
            });

            modelBuilder.Entity("TechSolutionsApp.Models.User", b =>
            {
                b.HasOne("TechSolutionsApp.Models.Role", "Role")
                    .WithMany("Users")
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.Navigation("Role");
            });

            modelBuilder.Entity("TechSolutionsApp.Models.Project").Navigation("Tasks");

            modelBuilder.Entity("TechSolutionsApp.Models.Role").Navigation("Users");

            modelBuilder.Entity("TechSolutionsApp.Models.User").Navigation("Projects");
#pragma warning restore 612, 618
        }
    }
}
