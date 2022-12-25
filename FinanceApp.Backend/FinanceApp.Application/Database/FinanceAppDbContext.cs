using FinanceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Application.Database;

public sealed class FinanceAppDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    public FinanceAppDbContext(DbContextOptions<FinanceAppDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}