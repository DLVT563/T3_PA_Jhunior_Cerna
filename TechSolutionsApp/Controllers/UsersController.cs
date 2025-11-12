using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechSolutionsApp.Data;
using TechSolutionsApp.Models;
using TechSolutionsApp.Utilities;

namespace TechSolutionsApp.Controllers;

[Authorize]
public class UsersController : Controller
{
    private readonly ApplicationDbContext _context;

    public UsersController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _context.Users.Include(u => u.Role).OrderBy(u => u.Name).ToListAsync();
        return View(users);
    }

    public IActionResult Create()
    {
        PopulateRolesDropDownList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Email,Password,RoleId")] User user)
    {
        if (await _context.Users.AnyAsync(u => u.Email == user.Email))
        {
            ModelState.AddModelError("Email", "El correo ya se encuentra registrado.");
        }

        if (string.IsNullOrWhiteSpace(user.Password))
        {
            ModelState.AddModelError("Password", "La contraseña es obligatoria.");
        }

        if (ModelState.IsValid)
        {
            user.PasswordHash = PasswordHasher.Hash(user.Password!);
            _context.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        PopulateRolesDropDownList(user.RoleId);
        return View(user);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        PopulateRolesDropDownList(user.RoleId);
        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password,RoleId")] User user)
    {
        if (id != user.Id)
        {
            return NotFound();
        }

        var existingUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        if (existingUser == null)
        {
            return NotFound();
        }

        if (await _context.Users.AnyAsync(u => u.Email == user.Email && u.Id != id))
        {
            ModelState.AddModelError("Email", "El correo ya se encuentra registrado.");
        }

        if (ModelState.IsValid)
        {
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.RoleId = user.RoleId;

            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                existingUser.PasswordHash = PasswordHasher.Hash(user.Password);
            }

            try
            {
                _context.Update(existingUser);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        PopulateRolesDropDownList(user.RoleId);
        return View(user);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(m => m.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool UserExists(int id) => _context.Users.Any(e => e.Id == id);

    private void PopulateRolesDropDownList(object? selectedRole = null)
    {
        ViewBag.RoleId = new SelectList(_context.Roles.OrderBy(r => r.Name).ToList(), "Id", "Name", selectedRole);
    }
}
