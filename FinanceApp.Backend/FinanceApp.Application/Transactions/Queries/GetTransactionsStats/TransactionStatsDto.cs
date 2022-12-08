﻿namespace FinanceApp.Application.Transactions.Queries.GetTransactionsStats;

public class TransactionStatsDto
{
    public string TimePeriod { get; set; } = null!;
    public decimal Amount { get; set; }
}