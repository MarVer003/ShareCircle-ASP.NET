using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareCircle.Models;

public class Vracilo
{
    [Key]
    public int ID { get; set; }

    [ForeignKey(nameof(Dolžnik))]
    public int ID_dolznika { get; set; }

    [ForeignKey(nameof(Skupina))]
    public int ID_skupine { get; set; }

    [Required]
    public int StevilkaVracila { get; set; }

    [Required]
    public float ZnesekVracila { get; set; }

    [Required]
    public DateTime DatumVracila { get; set; }

    // Navigacijske lastnosti
    public required Uporabnik Dolžnik { get; init; }
    public required Skupina Skupina { get; init; }
}
