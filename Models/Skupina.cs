using System.ComponentModel.DataAnnotations;


namespace ShareCircle.Models;

public class Skupina
{
    [Key]
    public int ID { get; set; }

    [Required]
    [MaxLength(50)]
    public required string ImeSkupine { get; set; }

    [Required]
    public DateTime DatumNastanka { get; set; }

    // Navigacijske lastnosti
    public ICollection<ClanSkupine>? ClanSkupine { get; set; }
    public ICollection<Strosek>? Stroski { get; set; }
    public ICollection<Vracilo>? Vracila { get; set; }

}