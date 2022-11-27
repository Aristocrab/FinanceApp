using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Enums;

namespace FinanceApp.Application;

public class DbSeeder
{
    private readonly FinanceAppDbContext _dbContext;

    public DbSeeder(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void SeedDb()
    {
        if(_dbContext.Transactions.Any()) return;

        var accounts = new Account[]
        {
            new Account
            {
                Id = new Guid("A616108E-6C16-4860-81AE-DFE27B0FE618"),
                Name = "Debit card"
            },
            new Account
            {
                Id = new Guid("7AD8E8A5-AEAF-4CB2-B569-72D7FD7E9339"),
                Name = "Cash"
            }
        };
        _dbContext.Accounts.AddRange(accounts);

        var categories = new Category[]
        {
            new Category
            {
                Id = new Guid("B726278E-3C6C-408E-AADC-E8A50FCC20EB"),
                Name = "Food"
            },
            new Category
            {
                Id = new Guid("5EFB4600-43DA-4A7C-9B1F-3DDE5E23A3E0"),
                Name = "Health"
            },
            new Category
            {
                Id = new Guid("FD3EF3E9-33A4-406F-B07F-BDE4BE7EFFC1"),
                Name = "Car"
            }
        };
        _dbContext.Categories.AddRange(categories);

        var transactions = new Transaction[]
        {
            new Transaction
            {
                Account = accounts[0],
                Category = categories[0],
                Amount = 500,
                Date = DateTime.Now,
                Description = "Pizza у Івана",
                Type = TransactionType.Expense
            }
        };
        _dbContext.Transactions.AddRange(transactions);

        _dbContext.SaveChanges();
    }
}