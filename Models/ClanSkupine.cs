using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareCircle.Models;

public class ClanSkupine
{
    public int ID { get; set; }

    //[ForeignKey(nameof(Uporabnik))]
    public int UporabnikID { get; set; }

    //[ForeignKey(nameof(Skupina))]
    public int SkupinaID { get; set; }

    public DateTime DatumPridruzitve { get; set; }

    public decimal Stanje { get; set; } = 0;

    // Navigacijske lastnosti
    public Uporabnik? Uporabnik { get; set; }
    public Skupina? Skupina { get; set; }
}