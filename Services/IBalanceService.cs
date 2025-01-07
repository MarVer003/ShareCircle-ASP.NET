namespace ShareCircle.Services;

public interface IBalanceService
{
    Task RecalculateBalancesAsync(int? SkupinaId);
}