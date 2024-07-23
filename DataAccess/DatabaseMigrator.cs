using Microsoft.Extensions.DependencyInjection;
using FluentMigrator.Runner;

namespace DataAccess
{
    public class DatabaseMigrator
    {
        public static void MigrateDb(string connectionString)
        {
            IServiceProvider serviceProvider = CreateServices(connectionString);
            UpdateDatabase(serviceProvider.CreateScope().ServiceProvider);
        }

        private static IServiceProvider CreateServices(string connectionString)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(DatabaseMigrator).Assembly).For.All())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
