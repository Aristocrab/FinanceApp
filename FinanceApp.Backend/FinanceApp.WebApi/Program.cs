using FinanceApp.Application;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FinanceAppDbContext>(options =>
{
    options.UseSqlite("Data Source=FinanceApp.db");
});
builder.Services.AddMediatR(typeof(FinanceAppDbContext).Assembly);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DbSeeder>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
    seeder.SeedDb();
}

app.UseCors(options =>
{
    options.AllowAnyMethod();
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
});

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();