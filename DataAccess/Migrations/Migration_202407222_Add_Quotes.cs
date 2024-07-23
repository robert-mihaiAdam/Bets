using FluentMigrator;

namespace DataAccess.Migrations
{
    [Migration(202407222)]
    public class Migration_202407222_Add_Quotes : Migration
    {
        public override void Up()
        {
            Alter.Table("BetQuotes")
                .AlterColumn("QuoteA").AsDecimal(5,2).WithDefaultValue(1.00);

            Alter.Table("BetQuotes")
                .AlterColumn("QuoteB").AsDecimal(5, 2).WithDefaultValue(1.00);

            Alter.Table("BetQuotes")
                .AlterColumn("QuoteX").AsDecimal(5, 2).WithDefaultValue(1.00);
        }

        public override void Down()
        {
            Alter.Table("BetQuotes")
                .AlterColumn("QuoteA").AsDecimal();

            Alter.Table("BetQuotes")
                .AlterColumn("QuoteB").AsDecimal();

            Alter.Table("BetQuotes")
                .AlterColumn("QuoteX").AsDecimal();
        }
    }
}
