namespace FinanceApp.Application.Transactions.Queries.GetTransactionsStats;

public class TransactionStatsDto
{
    public string TimePeriod { get; set; } = null!;
    public decimal ExpensesSum { get; set; }
    public decimal IncomeSum { get; set; }
}