using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Bets.Database;
using Bets.Constants;

namespace Bets.Utils
{
    internal class Utils
    {
        
        public void UpdateMigration(Type T)
        {
            using (var serviceProvider = CreateServices(T))
            using (var scope = serviceProvider.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
            }
        }

        public void RemoveMigration(Type T, long version)
        {
            using (var serviceProvider = CreateServices(T))
            using (var scope = serviceProvider.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateDown(version);
            }
        }

        private ServiceProvider CreateServices(Type T)
        {
            Console.WriteLine(T.ToString());
            ServiceCollection serviceCollection = new ServiceCollection();
            return serviceCollection
                            .AddFluentMigratorCore()
                            .ConfigureRunner(rb => rb
                                .AddSqlServer()
                                .WithGlobalConnectionString(Constants.Constants.connection_data)
                                .ScanIn(T.Assembly).For.Migrations())
                            .AddLogging(lb => lb.AddFluentMigratorConsole())
                            .BuildServiceProvider(false);
        }
    }
}
