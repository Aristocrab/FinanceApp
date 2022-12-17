using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Enums;

namespace FinanceApp.Application.Database;

public class DbSeeder
{
    private readonly FinanceAppDbContext _dbContext;

    public DbSeeder(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void SeedDb()
    {
        foreach (var entity in _dbContext.Transactions)
        {
            _dbContext.Transactions.Remove(entity);
        }
        foreach (var entity in _dbContext.Accounts)
        {
            _dbContext.Accounts.Remove(entity);
        }
        foreach (var entity in _dbContext.Categories)
        {
            _dbContext.Categories.Remove(entity);
        }

        var accounts = new[]
        {
            new Account
            {
                Id = new Guid("7AD8E8A5-AEAF-4CB2-B569-72D7FD7E9339"),
                Name = "Cash",
                Balance = 100,
                Currency = Currency.UAH,
                Icon = 0
            },
            new Account
            {
                Id = new Guid("A616108E-6C16-4860-81AE-DFE27B0FE618"),
                Name = "Debit card",
                Balance = 0, 
                Currency = Currency.USD,
                Icon = 1
            }
        };
        _dbContext.Accounts.AddRange(accounts);

        var categories = new[]
        {
            new Category
            {
                Id = new Guid("B726278E-3C6C-408E-AADC-E8A50FCC20EB"),
                Name = "Food"
            },
            new Category
            {
                Id = new Guid("5EFB4600-43DA-4A7C-9B1F-3DDE5E23A3E0"),
                Name = "Cafe"
            },
            new Category
            {
                Id = new Guid("FD3EF3E9-33A4-406F-B07F-BDE4BE7EFFC1"),
                Name = "Car"
            },
            new Category
            {
                Id = new Guid("8C46AD16-F09C-43E6-891C-674D4C421A32"),
                Name = "Family"
            }
        };
        _dbContext.Categories.AddRange(categories);

        var transactions = new[]
        {
            new Transaction
            {
                Account = accounts[0],
                Category = categories[0],
                Amount = 250,
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-2)),
                Description = "Піца у Івана",
                Type = TransactionType.Expense
            },
            new Transaction
            {
                Account = accounts[0],
                Category = categories[1],
                Amount = 360,
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                Description = "36 крилець KFC",
                Type = TransactionType.Expense
            },
            new Transaction
            {
                Account = accounts[1],
                Category = categories[1],
                Amount = 39,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Description = "Пиво",
                Type = TransactionType.Expense
            },
            new Transaction
            {
                Account = accounts[1],
                Category = categories[2],
                Amount = 100,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Description = "Зарплата",
                Type = TransactionType.Income
            },
            new Transaction
            {
                Account = accounts[1],
                Category = categories[2],
                Amount = 100,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Description = "Зарплата 2",
                Type = TransactionType.Income
            },
            new Transaction
            {
                Account = accounts[1],
                Category = categories[2],
                Amount = 100,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Description = "Зарплата 3",
                Type = TransactionType.Income
            },
            new Transaction
            {
                Account = accounts[1],
                Amount = 200,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Description = "Переказ",
                Type = TransactionType.Transfer
            },
        };
        _dbContext.Transactions.AddRange(transactions);

        _dbContext.SaveChanges();
    }
}