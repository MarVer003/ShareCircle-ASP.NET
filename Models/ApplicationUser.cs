using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ShareCircle.Models;

public class ApplicationUser : IdentityUser
{
    public string? Ime { get; set; }

    public string? Priimek { get; set; }

    public DateTime DatumPrijave { get; set; }

    public ICollection<Skupina>? Skupine { get; set; }
    public ICollection<ClanSkupine>? ClanSkupine { get; set; }
    public ICollection<Strosek>? Stroski { get; set; }
    public ICollection<Vracilo>? Vracila { get; set; }

    public ICollection<RazdelitevStroska>? RazdelitveStroskov { get; set; }
}
