using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Interfaces;
using Domain;
using Services.Facades;
using BetsApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DbDatabaseCreation>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IBetableEntityService, BetableEntityService>();
builder.Services.AddScoped<IBetQuoteService, BetQuoteService>();
builder.Services.AddScoped<IBetsService, BetsService>();
builder.Services.AddScoped<IPlacedBetsService, PlacedBetsService>();
builder.Services.AddScoped<IBetFacade, BetFacade>();
builder.Services.AddScoped<IPlacedBetFacade, PlacedBetFacade>();

builder.Services.AddExceptionHandler<GlobalErrorHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddSingleton<TimeProvider>(TimeProvider.System);

builder.Services.AddAutoMapper(typeof(MapperConfig).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/error-development");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var serviceScope = app.Services.CreateScope();
var context = serviceScope.ServiceProvider.GetRequiredService<DbDatabaseCreation>();
context.Database.EnsureCreated();
DatabaseMigrator.MigrateDb(connectionString);

app.Run();
