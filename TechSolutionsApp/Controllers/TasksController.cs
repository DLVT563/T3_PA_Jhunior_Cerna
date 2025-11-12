using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechSolutionsApp.Data;
using TechSolutionsApp.Models;

namespace TechSolutionsApp.Controllers;

[Authorize]
public class TasksController : Controller
{
    private readonly ApplicationDbContext _context;

    public TasksController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var tasks = await _context.Tasks.Include(t => t.Project).ThenInclude(p => p!.User).OrderBy(t => t.Title).ToListAsync();
        return View(tasks);
    }

    public IActionResult Create()
    {
        PopulateProjectsDropDownList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Status,ProjectId")] TaskItem taskItem)
    {
        if (ModelState.IsValid)
        {
            _context.Add(taskItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        PopulateProjectsDropDownList(taskItem.ProjectId);
        return View(taskItem);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var taskItem = await _context.Tasks.FindAsync(id);
        if (taskItem == null)
        {
            return NotFound();
        }

        PopulateProjectsDropDownList(taskItem.ProjectId);
        return View(taskItem);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Status,ProjectId")] TaskItem taskItem)
    {
        if (id != taskItem.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(taskItem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskItemExists(taskItem.Id))
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

        PopulateProjectsDropDownList(taskItem.ProjectId);
        return View(taskItem);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var taskItem = await _context.Tasks.Include(t => t.Project).ThenInclude(p => p!.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (taskItem == null)
        {
            return NotFound();
        }

        return View(taskItem);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var taskItem = await _context.Tasks.FindAsync(id);
        if (taskItem != null)
        {
            _context.Tasks.Remove(taskItem);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool TaskItemExists(int id) => _context.Tasks.Any(e => e.Id == id);

    private void PopulateProjectsDropDownList(object? selectedProject = null)
    {
        var projects = _context.Projects.Include(p => p.User).OrderBy(p => p.Name).ToList();
        ViewBag.ProjectId = new SelectList(projects, "Id", "Name", selectedProject);
    }
}
