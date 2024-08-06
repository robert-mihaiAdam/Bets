using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DbDatabaseCreation : DbContext
    {

        public DbDatabaseCreation(DbContextOptions<DbDatabaseCreation> options) : base(options)
        {
        }

        public DbDatabaseCreation(string connectionString) : base(GetOptions(connectionString))
        {

        }

        private static DbContextOptions<DbDatabaseCreation> GetOptions(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbDatabaseCreation>();
            optionsBuilder.UseSqlServer(connectionString);
            return optionsBuilder.Options;
        }

    }
}
