using Microsoft.Extensions.DependencyInjection;
using FluentMigrator.Runner;
using System;
using System.Linq;
using FluentMigrator.Runner.Initialization;
using Bets.Database;
using Microsoft.Data.SqlClient;
using Bets.Utils;
using Bets.Clients;
using Bets.Database;

namespace Bets
{
    class Program
    {
        static void Main(string[] args)
        {
            //Utils.Utils utils = new();
            //utils.UpdateMigration(typeof(Seeder));
            Clients.Clients clnts = new();
            clnts.startBet();
        }
    }
 }