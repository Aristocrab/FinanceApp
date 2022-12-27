using FinanceApp.Domain.Enums;

namespace FinanceApp.Application.Common.Helpers;

public static class ChangeCurrencyHelper
{
    private static readonly Dictionary<Currency, decimal> ExchangeRate = new()
    {
        { Currency.UAH, 1 },
        { Currency.USD, 36 },
        { Currency.EUR, 38 },
    };
    
    public static decimal ChangeCurrency(decimal amount, Currency from, Currency to)
    {
        return amount * ExchangeRate[from] / ExchangeRate[to];
    }
}