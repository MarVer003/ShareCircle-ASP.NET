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
    public string ID_dolznika { get; set; }


    [Required]
    public decimal Znesek { get; set; }

    // Navigacijske lastnosti
    public required Strosek Strosek { get; init; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public required ApplicationUser Dolznik { get; init; }

}
