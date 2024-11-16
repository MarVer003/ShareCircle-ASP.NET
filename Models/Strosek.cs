using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareCircle.Models;

public class Strosek
{
    [Key]
    public int ID { get; set; }

    [ForeignKey(nameof(Placnik))]
    public int ID_placnika { get; set; }

    [ForeignKey(nameof(Skupina))]
    public int ID_skupine { get; set; }

    public int StevilkaStroska { get; set; }

    [Required]
    public float CelotniZnesek { get; set; }

    [Required]
    public DateTime DatumPlacila { get; set; }

   // Navigacijske lastnosti
    public required Uporabnik Placnik { get; init; }
    public required Skupina Skupina { get; init; }
    public ICollection<RazdelitevStroska>? RazdelitveStroskov { get; set; }
}
