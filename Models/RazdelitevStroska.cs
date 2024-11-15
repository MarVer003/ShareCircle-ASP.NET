using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareCircle.Models;

public class RazdelitevStroska
{
    [Key, Column(Order = 0)]
    public int ID_stroska { get; set; }

    [Key, Column(Order = 1)]
    public int ID_dolznika { get; set; }

    [Required]
    public float Znesek { get; set; }

    // Navigacijske lastnosti
    [ForeignKey("IDStroška")]
    public Strosek Strosek { get; set; }
    [ForeignKey("IDDolžnika")]
    public Uporabnik Dolžnik { get; set; }

}
