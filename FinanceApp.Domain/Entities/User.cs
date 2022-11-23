﻿namespace FinanceApp.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public List<Transaction> Transactions { get; set; }
}