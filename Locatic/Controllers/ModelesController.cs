using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Locatic.Data;
using Locatic.Models;

namespace Locatic.Controllers;

public class ModelesController : Controller
{
    private readonly AppDbContext _context;

    public ModelesController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var modeles = _context.Modeles
            .Include(m => m.Marque)
            .ToList();

        return View(modeles);
    }

    public IActionResult Create()
    {
        ViewBag.Marques = new SelectList(_context.Marques, "Id", "Nom");

        return View();
    }

    [HttpPost]
    public IActionResult Create(Modele modele)
    {
        if (ModelState.IsValid)
        {
            _context.Modeles.Add(modele);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        ViewBag.Marques = new SelectList(_context.Marques, "Id", "Nom");

        return View(modele);
    }
}