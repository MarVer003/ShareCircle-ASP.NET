using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareCircle.Models;

public class ClanSkupine
{
    [Key, Column(Order = 0)]
    public int ID_uporabnika { get; set; }

    [Key, Column(Order = 1)]
    public int ID_skupine { get; set; }

    [Required]
    public DateTime DatumPridruzitve { get; set; }

    // Navigacijske lastnosti
    [ForeignKey("ID_uporabnika")]
    public Uporabnik Uporabnik { get; set; }

    [ForeignKey("ID_skupine")]
    public Skupina Skupina { get; set; }
}