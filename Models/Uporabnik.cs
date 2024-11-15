using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ShareCircle.Models;

public class Uporabnik
{
    [Key]
    public int ID { get; set; }

    [Required]
    [MaxLength(50)]
    public string Ime { get; set; }

    [Required]
    [MaxLength(50)]
    public string Priimek { get; set; }

    [Required]
    public DateTime DatumPrijave { get; set; }

     // Navigacijske lastnosti
    public ICollection<ClanSkupine> ClanSkupine { get; set; }
    public ICollection<Strosek> Stroski { get; set; }
    public ICollection<RazdelitevStroska> RazdelitveStroskov { get; set; }
    public ICollection<Vracilo> Vracila { get; set; }
}
