using System.ComponentModel.DataAnnotations;

namespace Locatic.Models;

public class Marque
{
    public int Id { get; set; }

    [Required]
    public string Nom { get; set; } = "";

    public List<Modele>? Modeles { get; set; }
}