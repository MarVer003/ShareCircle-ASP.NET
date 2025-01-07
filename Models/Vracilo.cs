using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareCircle.Models;

public class Vracilo
{
    public int ID { get; set; }

    [ForeignKey(nameof(Dolžnik))]
    public string ID_dolznika { get; set; }

    [ForeignKey(nameof(Upnik))]
    public string ID_upnika { get; set; }

    [ForeignKey(nameof(Skupina))]
    public int ID_skupine { get; set; }

    public int StevilkaVracila { get; set; }

    public decimal ZnesekVracila { get; set; }

    public DateTime DatumVracila { get; set; }

    // Navigacijske lastnosti
    public ApplicationUser? Dolžnik { get; init; }
    public ApplicationUser? Upnik { get; init; }
    public Skupina? Skupina { get; init; }
}
