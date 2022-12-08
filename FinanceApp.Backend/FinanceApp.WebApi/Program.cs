using FinanceApp.Application;
using FinanceApp.WebApi.Middleware.CustomExpectionsHandler;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
    seeder.SeedDb();
}

app.UseCustomExceptionsHandler();

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