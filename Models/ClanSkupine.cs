using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareCircle.Models;

public class ClanSkupine
{   
    [Key]
    public int ID { get; set; }

    [ForeignKey(nameof(Uporabnik))]
    public int ID_uporabnika { get; set; }

    [ForeignKey(nameof(Skupina))]
    public int ID_skupine { get; set; }

    [Required]
    public DateTime DatumPridruzitve { get; set; }

    // Navigacijske lastnosti
    public required Uporabnik Uporabnik { get; init; }
    public required Skupina Skupina { get; init; }
}