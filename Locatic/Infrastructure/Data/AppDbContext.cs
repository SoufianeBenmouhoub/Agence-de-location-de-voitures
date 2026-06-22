using Microsoft.EntityFrameworkCore;
using Locatic.Models;

namespace Locatic.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Marque> Marques { get; set; }

    public DbSet<Modele> Modeles { get; set; }

    public DbSet<Voiture> Voitures { get; set; }

    public DbSet<Client> Clients { get; set; }

    public DbSet<Reservation> Reservations { get; set; }
}