using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Locatic.Data;
using Locatic.Models;

namespace Locatic.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly AppDbContext _context;

        public ReservationsController(AppDbContext context)
        {
            _context = context;
        }

        // LISTE
        public IActionResult Index()
        {
            var reservations = _context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Voiture)
                .ToList();

            return View(reservations);
        }

        // FORM CREATE
        public IActionResult Create()
        {
            ViewBag.Clients = new SelectList(_context.Clients, "Id", "Nom");
            ViewBag.Voitures = new SelectList(_context.Voitures, "Id", "Immatriculation");

            return View();
        }

        // POST CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reservation reservation)
        {
            Console.WriteLine("POST EXECUTÉ");
            Console.WriteLine("ModelState = " + ModelState.IsValid);

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine("ERROR: " + error.ErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Clients = new SelectList(_context.Clients, "Id", "Nom");
                ViewBag.Voitures = new SelectList(_context.Voitures, "Id", "Immatriculation");
                return View(reservation);
            }

            var voiture = _context.Voitures.FirstOrDefault(v => v.Id == reservation.VoitureId);

            if (voiture == null)
            {
                ModelState.AddModelError("", "Voiture introuvable");
                return View(reservation);
            }

            var jours = (reservation.DateFin - reservation.DateDebut).Days;

            if (jours <= 0)
            {
                ModelState.AddModelError("", "Dates invalides");
                return View(reservation);
            }

            reservation.PrixTotal = jours * voiture.TarifJournalier;

            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}