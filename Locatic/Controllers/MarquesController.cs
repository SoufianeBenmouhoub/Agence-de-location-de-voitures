using Microsoft.AspNetCore.Mvc;
using Locatic.Data;
using Locatic.Models;

namespace Locatic.Controllers;

public class MarquesController : Controller
{
    private readonly AppDbContext _context;

    public MarquesController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var marques = _context.Marques.ToList();
        return View(marques);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Marque marque)
    {
        if (ModelState.IsValid)
        {
            _context.Marques.Add(marque);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        return View(marque);
    }
}