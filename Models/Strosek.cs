using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareCircle.Models;

public class Strosek
{
    [Key, Column(Order = 0)]
    public int ID_placnika { get; set; }

    [Key, Column(Order = 1)]
    public int ID_skupine { get; set; }

    [Key, Column(Order = 2)]
    public int StevilkaStroska { get; set; }

    [Required]
    public float CelotniZnesek { get; set; }

    [Required]
    public DateTime DatumPlacila { get; set; }

   // Navigacijske lastnosti
    [ForeignKey("IDPlačnika")]
    public Uporabnik Uporabnik { get; set; }
    [ForeignKey("IDSkupine")]
    public Skupina Skupina { get; set; }
    public ICollection<RazdelitevStroska> RazdelitveStroskov { get; set; }
}
