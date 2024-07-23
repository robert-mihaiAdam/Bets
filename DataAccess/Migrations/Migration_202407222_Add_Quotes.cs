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
            .WithColumn("QuoteA").AsDecimal().NotNullable()
            .WithColumn("QuoteB").AsDecimal().NotNullable()
            .WithColumn("QuoteX").AsDecimal().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("BetQuotes");
        }
    }
}
