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
        if (_dbContext.Users.Any(x => x.Id == new Guid("D3AC2B50-6CD3-4D38-8EC8-C8D3827FB3EF")))
        {
            foreach (var transaction in _dbContext.Transactions)
            {
                _dbContext.Transactions.Remove(transaction);
            }
            foreach (var account in _dbContext.Accounts)
            {
                _dbContext.Accounts.Remove(account);
            }
            foreach (var category in _dbContext.Categories)
            {
                _dbContext.Categories.Remove(category);
            }
        }
        
        // demo user
        var user = new User
        {
            Id = new Guid("D3AC2B50-6CD3-4D38-8EC8-C8D3827FB3EF"),
            Username = "user",
            Password = "pass"
        };
        _dbContext.Users.Add(user);
        
        var accounts = new[]
        {
            new Account
            {
                Id = new Guid("7AD8E8A5-AEAF-4CB2-B569-72D7FD7E9339"),
                Name = "Cash",
                Balance = 100,
                Currency = Currency.UAH,
                Icon = 0,
                User = user
            },
            new Account
            {
                Id = new Guid("A616108E-6C16-4860-81AE-DFE27B0FE618"),
                Name = "Debit card",
                Balance = 0, 
                Currency = Currency.USD,
                Icon = 1,
                User = user
            }
        };
        _dbContext.Accounts.AddRange(accounts);

        var categories = new[]
        {
            new Category
            {
                Id = new Guid("B726278E-3C6C-408E-AADC-E8A50FCC20EB"),
                Name = "Food",
                User = user
            },
            new Category
            {
                Id = new Guid("5EFB4600-43DA-4A7C-9B1F-3DDE5E23A3E0"),
                Name = "Cafe",
                User = user
            },
            new Category
            {
                Id = new Guid("FD3EF3E9-33A4-406F-B07F-BDE4BE7EFFC1"),
                Name = "Car",
                User = user
            },
            new Category
            {
                Id = new Guid("8C46AD16-F09C-43E6-891C-674D4C421A32"),
                Name = "Family",
                User = user
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
                Type = TransactionType.Expense,
                User = user
            },
            new Transaction
            {
                Account = accounts[0],
                Category = categories[1],
                Amount = 360,
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                Description = "36 крилець KFC",
                Type = TransactionType.Expense,
                User = user
            },
            new Transaction
            {
                Account = accounts[1],
                Category = categories[1],
                Amount = 39,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Description = "Пиво",
                Type = TransactionType.Expense,
                User = user
            },
            new Transaction
            {
                Account = accounts[1],
                Category = categories[2],
                Amount = 100,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Description = "Зарплата",
                Type = TransactionType.Income,
                User = user
            },
            new Transaction
            {
                Account = accounts[1],
                Category = categories[2],
                Amount = 100,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Description = "Зарплата 2",
                Type = TransactionType.Income,
                User = user
            },
            new Transaction
            {
                Account = accounts[1],
                Category = categories[2],
                Amount = 100,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Description = "Зарплата 3",
                Type = TransactionType.Income,
                User = user
            },
            new Transaction
            {
                Account = accounts[1],
                Amount = 200,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Description = "Переказ",
                Type = TransactionType.Transfer,
                User = user
            },
        };
        _dbContext.Transactions.AddRange(transactions);

        _dbContext.SaveChanges();
    }
}