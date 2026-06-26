namespace Locatic.Models;

public class Reservation
{
    public int Id { get; set; }

    public int ClientId { get; set; }
    public Client? Client { get; set; }

    public int VoitureId { get; set; }
    public Voiture? Voiture { get; set; }

    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }

    public decimal PrixTotal { get; set; }
}