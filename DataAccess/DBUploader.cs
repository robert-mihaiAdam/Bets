using Microsoft.Extensions.DependencyInjection;
using FluentMigrator.Runner;
using DataAccess.TableCreator;

namespace DataAccess
{
    public class DBUploader
    {
        public static void uploadDB()
        {
            IServiceProvider serviceProvider = CreateServices();
            UpdateDatabase(serviceProvider.CreateScope().ServiceProvider);
        }

        private static IServiceProvider CreateServices()
        {
            
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(Constants.Constants.connection_data)
                    .ScanIn(typeof(BetableEntityCreator).Assembly).For.Migrations())
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
