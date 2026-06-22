using System.ComponentModel.DataAnnotations;

namespace Locatic.Models;

public class Modele
{
    public int Id { get; set; }

    [Required]
    public string Nom { get; set; } = "";

    public int MarqueId { get; set; }

    public Marque? Marque { get; set; }

    public List<Voiture>? Voitures { get; set; }
}