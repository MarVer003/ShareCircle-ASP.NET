using System.ComponentModel.DataAnnotations;


namespace ShareCircle.Models;

public class Skupina
{
    public int ID { get; set; }

    public string? ImeSkupine { get; set; }

    public DateTime DatumNastanka { get; set; }

    // Navigacijske lastnosti
    public ICollection<ClanSkupine>? ClanSkupine { get; set; }
    public ICollection<Strosek>? Stroski { get; set; }
    public ICollection<Vracilo>? Vracila { get; set; }

}