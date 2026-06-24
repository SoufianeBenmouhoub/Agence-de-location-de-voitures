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
            .Include(v => v.Modele);

        return View(voitures.ToList());
    }

    public IActionResult Create()
    {
        ViewBag.Modeles =
            new SelectList(_context.Modeles, "Id", "Nom");

        return View();
    }

    [HttpPost]
    public IActionResult Create(Voiture voiture)
    {
        if (ModelState.IsValid)
        {
            _context.Voitures.Add(voiture);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        ViewBag.Modeles =
            new SelectList(_context.Modeles, "Id", "Nom");

        return View(voiture);
    }
}