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

    public IActionResult Index()
    {
        var voitures = _context.Voitures
            .Include(v => v.Modele)
            .ThenInclude(m => m.Marque)
            .ToList();

        return View(voitures);
    }

    public IActionResult Details(int id)
    {
        var voiture = _context.Voitures
            .Include(v => v.Modele)
            .ThenInclude(m => m.Marque)
            .FirstOrDefault(v => v.Id == id);

        return View(voiture);
    }

    public IActionResult Create()
    {
        ViewBag.Modeles = new SelectList(_context.Modeles, "Id", "Nom");
        return View();
    }

    [HttpPost]
    public IActionResult Create(Voiture voiture)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Modeles = new SelectList(_context.Modeles, "Id", "Nom");
            return View(voiture);
        }

        _context.Voitures.Add(voiture);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var voiture = _context.Voitures.Find(id);

        ViewBag.Modeles = new SelectList(_context.Modeles, "Id", "Nom", voiture.ModeleId);

        return View(voiture);
    }

    [HttpPost]
    public IActionResult Edit(Voiture voiture)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Modeles = new SelectList(_context.Modeles, "Id", "Nom", voiture.ModeleId);
            return View(voiture);
        }

        _context.Voitures.Update(voiture);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var voiture = _context.Voitures
            .Include(v => v.Modele)
            .FirstOrDefault(v => v.Id == id);

        return View(voiture);
    }

    [HttpPost]
    public IActionResult Delete(Voiture voiture)
    {
        _context.Voitures.Remove(voiture);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}