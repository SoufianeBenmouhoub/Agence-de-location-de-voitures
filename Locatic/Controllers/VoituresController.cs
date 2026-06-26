using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Locatic.Data;
using Locatic.Models;

namespace Locatic.Controllers;

public class VoituresController : Controller
{
    private readonly AppDbContext _context;

    public VoituresController(AppDbContext context)
    {
        _context = context;
    }

    // Liste des voitures
    public IActionResult Index()
    {
        var voitures = _context.Voitures
            .Include(v => v.Modele)
            .ThenInclude(m => m.Marque)
            .ToList();

        return View(voitures);
    }

    // Détails d'une voiture
    public IActionResult Details(int id)
    {
        var voiture = _context.Voitures
            .Include(v => v.Modele)
            .ThenInclude(m => m.Marque)
            .FirstOrDefault(v => v.Id == id);

        if (voiture == null)
        {
            return NotFound();
        }

        return View(voiture);
    }

    // Formulaire d'ajout
    public IActionResult Create()
    {
        ViewBag.Modeles = new SelectList(_context.Modeles, "Id", "Nom");

        return View();
    }

    // Enregistrer une voiture
    [HttpPost]
    public IActionResult Create(Voiture voiture)
    {
        if (ModelState.IsValid)
        {
            _context.Voitures.Add(voiture);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        ViewBag.Modeles = new SelectList(_context.Modeles, "Id", "Nom");

        return View(voiture);
    }
}