using FluentMigrator;

namespace DataAccess.Migrations
{
    [Migration(202408062)]
    public class Migration_202408062_BetQuote_AddIndex : Migration
    {
        public override void Up()
        {
            Create.Index("IX_BetQuotes_BetId")
                  .OnTable("BetQuotes")
                  .OnColumn("BetId")
                  .Ascending()
                  .WithOptions().Unique();
        }

        public override void Down()
        {
            Delete.Index("IX_BetQuotes_BetId").OnTable("BetId");
        }
    }
}
