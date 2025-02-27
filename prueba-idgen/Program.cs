using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PruebaIdGen.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSqlite<ApplicationDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
//builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddDbContext<ApplicationDbContext>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole())));

var app = builder.Build();
 
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

// Setup Databases
using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    serviceScope.ServiceProvider.GetService<ApplicationDbContext>()!.Database.EnsureDeleted();
    serviceScope.ServiceProvider.GetService<ApplicationDbContext>()!.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


[ExcludeFromCodeCoverage] partial class Program {}