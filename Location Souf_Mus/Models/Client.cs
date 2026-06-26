using System.ComponentModel.DataAnnotations;

namespace Locatic.Models;

public class Client
{
    public int Id { get; set; }

    [Required]
    public string Nom { get; set; } = "";

    [Required]
    public string Prenom { get; set; } = "";

    [EmailAddress]
    public string Email { get; set; } = "";

    public string Telephone { get; set; } = "";

    public List<Reservation>? Reservations { get; set; }
}