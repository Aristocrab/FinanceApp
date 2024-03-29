﻿using FinanceApp.Domain.Enums;
using MediatR;

namespace FinanceApp.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid AccountId { get; set; }
    
    public decimal Amount { get; set; }
    public string Description { get; set; } = null!;
    public TransactionType Type { get; set; }
    public DateOnly Date { get; set; }
}