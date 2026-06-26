using System.ComponentModel.DataAnnotations;

namespace Locatic.Models;

public class Voiture
{
    public int Id { get; set; }

    [Required]
    public string Immatriculation { get; set; } = "";

    public int Annee { get; set; }

    public decimal TarifJournalier { get; set; }

    public int NombrePlaces { get; set; }

    public string Carburant { get; set; } = "";

    public int ModeleId { get; set; }

    public Modele? Modele { get; set; }

    public List<Reservation>? Reservations { get; set; }
}