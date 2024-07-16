using FluentMigrator;

namespace DataAccess.Migrations
{
    [Migration(202407155)]
    public class Migration_202407155_Add_Quotes : Migration
    {
        public override void Down()
        {
            Delete.Table("BetQuotes");
        }

        public override void Up()
        {
            if (!Schema.Table("BetQuotes").Exists())
            {
                Create.Table("BetQuotes")
                 .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                 .WithColumn("BetId").AsGuid()
                 .WithColumn("QuoteA").AsDecimal()
                 .WithColumn("QuoteB").AsDecimal()
                 .WithColumn("QuoteX").AsDecimal();
            }
        }
    }
}
