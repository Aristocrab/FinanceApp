﻿using MediatR;

namespace FinanceApp.Application.Transactions.Commands.TransferTransaction;

public class TransferTransactionCommand : IRequest<Guid>
{
    public Guid AccountFromId { get; set; }
    public Guid AccountToId { get; set; }
    
    public decimal Amount { get; set; }
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
}