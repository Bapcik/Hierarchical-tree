using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testProject.Models;

namespace testProject.Controllers;

public class HomeController : Controller
{
    ApplicationContext db;
    public HomeController(ApplicationContext context)
    {
        db = context;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        
        return View();
    }

    public async Task<IActionResult> GetRoot()
    {
        var root = await db.HierarchyNodes.Where(n => n.ParentId == null).OrderBy(n => n.Id).Select(n => new
        {
            n.Id,
            n.Name,
            n.ParentId,
            HasChildren = db.HierarchyNodes.Any(c => c.ParentId == n.Id)
        }).ToListAsync();
        return Json(root);
    }

    [HttpGet]
    public async Task<IActionResult> GetChildren(int Id)
    {
        var children = await db.HierarchyNodes
            .Where(n => n.ParentId == Id)
            .Select(n => new
            {
                n.Id,
                n.Name,
                n.ParentId,
                HasChildren = db.HierarchyNodes.Any(c => c.ParentId == n.Id)
            })
            .ToListAsync();

        return Json(children);
    }

    // POST
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(HierarchyModel node)
    {
        if (node.ParentId == null)
        {
            return View(node);
        }

        bool parentExists = await db.HierarchyNodes.AnyAsync(n => n.Id == node.ParentId);

        if (!parentExists)
        {
            return View(node);
        }
        db.HierarchyNodes.Add(node);
        await db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    // EDIT
    public async Task<IActionResult> Edit(int? id)
    {

        if (id != null)
        {
            HierarchyModel? node = await db.HierarchyNodes.FirstOrDefaultAsync(p => p.Id == id);
            if (node != null) return View(node);
        }
        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> Edit(HierarchyModel node)
    {
        if (node.ParentId == null)
        {
            return View(node);
        }

        bool parentExists = await db.HierarchyNodes.AnyAsync(n => n.Id == node.ParentId);

        if (!parentExists)
        {
            return View(node);
        }

        db.HierarchyNodes.Update(node);
        await db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    // DELETE
    [HttpPost]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var node = await db.HierarchyNodes.FindAsync(id);

        if (node == null)
            return NotFound();

        db.HierarchyNodes.Remove(node);
        await db.SaveChangesAsync();

        return RedirectToAction("Index");
    }

}
