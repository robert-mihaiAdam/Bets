using Microsoft.Extensions.DependencyInjection;
using FluentMigrator.Runner;
using DataAccess.Migrations;
using Domain.Dto;

namespace DataAccess
{
    public class DatabaseMigrator
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
                    .WithGlobalConnectionString(Abstraction.connection_data)
                    .ScanIn(typeof(Migration_202407154_Add_Entities).Assembly).For.Migrations())
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
