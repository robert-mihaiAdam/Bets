using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IBetableEntityService, BetableEntityService>();
builder.Services.AddScoped<IBetQuoteService, BetQuoteService>();
builder.Services.AddScoped<IBetsService, BetsService>();
builder.Services.AddScoped<IPlacedBetsService, PlacedBetsService>();

builder.Services.AddSingleton<TimeProvider>(TimeProvider.System);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
DatabaseMigrator.MigrateDb();

app.Run();
