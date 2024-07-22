using FluentMigrator;

namespace DataAccess.Migrations
{
    [Migration(202407222)]
    public class Migration_202407222_Add_Quotes : Migration
    {
        public override void Up()
        {
            Create.Table("BetQuotes")
            .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("BetId").AsGuid().NotNullable()
            .WithColumn("QuoteA").AsDecimal(5, 2).NotNullable()
            .WithColumn("QuoteB").AsDecimal(5, 2).NotNullable()
            .WithColumn("QuoteX").AsDecimal(5, 2).NotNullable();

            Execute.Sql(
               "ALTER TABLE BetQuotes " +
               "ADD CONSTRAINT CHK_QUOTE " +
               "CHECK ((QuoteA >= 1.00 AND QuoteA <= 100.00) " +
               "AND (QuoteB >= 1.00 AND QuoteB <= 100.00) " +
               "AND (QuoteX >= 1.00 AND QuoteX <= 100.00))"
           );
        }

        public override void Down()
        {
            Execute.Sql("ALTER TABLE BetQuotes DROP CONSTRAINT CHK_QUOTE");
            Delete.Table("BetQuotes");
        }
    }
}
