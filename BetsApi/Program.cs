using DataAccess;
using Domain.Command;
using Domain.Dto;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IBetsService<Bets, UpdateBets>, BetsServices>();
builder.Services.AddScoped<IBetableEntityService<BetableEntity, UpdateBetableEntity>, BetableEntityServices>();
builder.Services.AddScoped<IBetQuoteService<BetQuotes, UpdateBetQuotes>, BetQuoteServices>();
builder.Services.AddScoped<IPlacedBetsService<PlacedBets, UpdatePlacedBets>, PlacedBetsService>();


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
