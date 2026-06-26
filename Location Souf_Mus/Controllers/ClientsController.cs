using Microsoft.AspNetCore.Mvc;
using Locatic.Data;
using Locatic.Models;

namespace Locatic.Controllers;

public class ClientsController : Controller
{
    private readonly AppDbContext _context;

    public ClientsController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.Clients.ToList());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Client client)
    {
        if (ModelState.IsValid)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(client);
    }
}