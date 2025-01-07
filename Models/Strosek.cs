using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareCircle.Models;

public class Strosek
{
    //[Key]
    public int ID { get; set; }

    [ForeignKey(nameof(Placnik))]
    public string ID_placnika { get; set; }

    [ForeignKey(nameof(Skupina))]
    public int ID_skupine { get; set; }

    public int StevilkaStroska { get; set; }

    public string? Naslov { get; set; }

    public decimal CelotniZnesek { get; set; }

    public DateTime DatumPlacila { get; set; }

   // Navigacijske lastnosti
    public ApplicationUser? Placnik { get; init; }
    public Skupina? Skupina { get; init; }
    public ICollection<RazdelitevStroska>? RazdelitveStroskov { get; set; }
}
