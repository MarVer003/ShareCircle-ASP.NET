using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace ShareCircle.Models;

public class Uporabnik
{
    public int ID { get; set; }

    public string? Ime { get; set; }

    public string? Priimek { get; set; }

    public DateTime DatumPrijave { get; set; }

    public ICollection<ClanSkupine>? ClanSkupine { get; set; }
    public ICollection<Strosek>? Stroski { get; set; }
    public ICollection<Vracilo>? Vracila { get; set; }
    
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public ICollection<RazdelitevStroska>? RazdelitveStroskov { get; set; }
}
