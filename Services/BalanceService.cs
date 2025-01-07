
using Microsoft.EntityFrameworkCore;
using ShareCircle.Data;

namespace ShareCircle.Services;

public class BalanceService : IBalanceService
{
    private readonly ShareCircleDbContext _context;

    public BalanceService(ShareCircleDbContext context)
    {
        _context = context;
    }

    public async Task RecalculateBalancesAsync(int? SkupinaId)
    {
        if (SkupinaId == null) return;

        var claniSkupine = await _context.ClanSkupine
            .Where(cs => cs.SkupinaID == SkupinaId)
            .ToArrayAsync();

        var stroskiInRazdelitve = await _context.Strosek
            .Include(s => s.RazdelitveStroskov)
            .Where(cs => cs.ID_skupine == SkupinaId)
            .ToArrayAsync();

        var vracila = await _context.Vracilo
            .Where(s => s.ID_skupine == SkupinaId)
            .ToArrayAsync();

        Dictionary<string, decimal> claniZneski = [];
        foreach (var clan in claniSkupine)
        {
            claniZneski.Add(clan.UporabnikID, 0);
        }
        foreach (var strosek in stroskiInRazdelitve)
        {
            if (strosek.RazdelitveStroskov == null) return;
            claniZneski[strosek.ID_placnika] += strosek.CelotniZnesek;
            foreach (var razdelitev in strosek.RazdelitveStroskov)
            {
                claniZneski[razdelitev.ID_dolznika] -= razdelitev.Znesek;
            }
        }
        foreach (var vracilo in vracila)
        {
            claniZneski[vracilo.ID_dolznika] += vracilo.ZnesekVracila;
            claniZneski[vracilo.ID_upnika] -= vracilo.ZnesekVracila;
        }

        foreach (var clan in claniSkupine)
        {
            clan.Stanje = claniZneski[clan.UporabnikID];
            _context.Update(clan);
        }
        await _context.SaveChangesAsync();
    }
}