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

        public IActionResult Index()
        {
            var reservations = _context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Voiture)
                    .ThenInclude(v => v.Modele)
                .ToList();

            return View(reservations);
        }

        public IActionResult Create()
        {
            ViewBag.Clients = new SelectList(_context.Clients, "Id", "Nom");

            ViewBag.Voitures = new SelectList(
                _context.Voitures
                    .Include(v => v.Modele)
                    .Select(v => new
                    {
                        Id = v.Id,
                        Nom = v.Modele.Nom
                    })
                    .ToList(),
                "Id",
                "Nom"
            );

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reservation reservation)
        {
            void Reload()
            {
                ViewBag.Clients = new SelectList(_context.Clients, "Id", "Nom");

                ViewBag.Voitures = new SelectList(
                    _context.Voitures
                        .Include(v => v.Modele)
                        .Select(v => new
                        {
                            Id = v.Id,
                            Nom = v.Modele.Nom
                        })
                        .ToList(),
                    "Id",
                    "Nom"
                );
            }

            if (reservation.DateFin <= reservation.DateDebut)
            {
                ModelState.AddModelError("", "La date de fin doit être après la date de début.");
                Reload();
                return View(reservation);
            }

            var voiture = _context.Voitures
                .FirstOrDefault(v => v.Id == reservation.VoitureId);

            if (voiture == null)
            {
                ModelState.AddModelError("", "Voiture introuvable.");
                Reload();
                return View(reservation);
            }

            bool overlap = _context.Reservations.Any(r =>
                r.VoitureId == reservation.VoitureId &&
                reservation.DateDebut < r.DateFin &&
                reservation.DateFin > r.DateDebut
            );

            if (overlap)
            {
                ModelState.AddModelError("", "Cette voiture est déjà réservée sur cette période.");
                Reload();
                return View(reservation);
            }

            var jours = (reservation.DateFin - reservation.DateDebut).Days;
            reservation.PrixTotal = jours * voiture.TarifJournalier;

            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}