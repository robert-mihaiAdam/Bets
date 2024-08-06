using FluentMigrator;

namespace DataAccess.Migrations
{
    [Migration(202408061)]
    public class Migration_202408061_Bets_AddIndex : Migration
    {
        public override void Up()
        {
            Create.Index("IX_Bets_BetableEntityA")
                  .OnTable("Bets")
                  .OnColumn("BetableEntityA")
                  .Ascending();


            Create.Index("IX_Bets_BetableEntityB")
                  .OnTable("Bets")
                  .OnColumn("BetableEntityB")
                  .Ascending();

        }

        public override void Down()
        {
            Delete.Index("IX_Bets_BetableEntityA").OnTable("Bets");
        }
    }
}
