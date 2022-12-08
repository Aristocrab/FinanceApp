using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceApp.Application;

public static class ConfigureServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddDbContext<FinanceAppDbContext>(options =>
        {
            options.UseSqlite("Data Source=FinanceApp.db");
        });
        services.AddMediatR(typeof(FinanceAppDbContext).Assembly); // assembies todo
        services.AddScoped<DbSeeder>();
        services.AddValidatorsFromAssembly(typeof(FinanceAppDbContext).Assembly);
        ValidatorOptions.Global.LanguageManager.Enabled = false;
    }
}