using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using Services;
using Domain;

namespace UnitTesting.SetupUnitTests
{
    public class DependencyInjectionFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }
        private string connectionString = "Server=localhost;Database=TestDatabase;TrustServerCertificate=true;Trusted_Connection=true;";

        public DependencyInjectionFixture()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            Seed();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DbDatabaseCreation>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<DBContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IBetableEntityService, BetableEntityService>();
            services.AddScoped<IBetQuoteService, BetQuoteService>();
            services.AddScoped<IBetsService, BetsService>();
            services.AddScoped<IPlacedBetsService, PlacedBetsService>();

            services.AddSingleton(TimeProvider.System);

            services.AddAutoMapper(typeof(MapperConfig).Assembly);
        }

        public async Task Seed()
        {
            var serviceScope = ServiceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<DbDatabaseCreation>();
            context.Database.EnsureCreated();
            DatabaseMigrator.MigrateDb(connectionString);
        }

        public async Task DisposeAsync()
        {
            var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetService<DbDatabaseCreation>();
            //await context.Database.EnsureDeletedAsync();
        }
    }
}
