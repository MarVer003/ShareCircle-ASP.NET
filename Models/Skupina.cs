using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ShareCircle.Models;

public class Skupina
{
    public int ID { get; set; }

    public string ImeSkupine { get; set; }

/*     [ForeignKey(nameof(Lastnik))]
    public string ID_lastnika { get; set; } */

    public DateTime DatumNastanka { get; set; }

    // Navigacijske lastnosti
    /* public ApplicationUser Lastnik { get; set; } */
    public ICollection<ClanSkupine>? ClanSkupine { get; set; }
    public ICollection<Strosek>? Stroski { get; set; }
    public ICollection<Vracilo>? Vracila { get; set; }

}