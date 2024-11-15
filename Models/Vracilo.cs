using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareCircle.Models;

public class Vracilo
{
    [Key, Column(Order = 0)]
    public int ID_dolznika { get; set; }

    [Key, Column(Order = 1)]
    public int ID_skupine { get; set; }

    [Key, Column(Order = 2)]
    public int StevilkaVracila { get; set; }

    [Required]
    public float ZnesekVracila { get; set; }

    [Required]
    public DateTime DatumVracila { get; set; }

    // Navigacijske lastnosti
    [ForeignKey("IDDolžnika")]
    public Uporabnik Dolžnik { get; set; }
    [ForeignKey("IDSkupine")]
    public Skupina Skupina { get; set; }
}
