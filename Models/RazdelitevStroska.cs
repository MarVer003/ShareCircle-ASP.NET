using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ShareCircle.Models;

public class RazdelitevStroska
{
    [Key]
    public int ID { get; set; }

    [ForeignKey(nameof(Strosek))]
    public int ID_stroska { get; set; }

    [ForeignKey(nameof(Dolznik))]
    public int ID_dolznika { get; set; }

    [Required]
    public float Znesek { get; set; }

    // Navigacijske lastnosti
    public required Strosek Strosek { get; init; }
    public required Uporabnik Dolznik { get; init; }

}
