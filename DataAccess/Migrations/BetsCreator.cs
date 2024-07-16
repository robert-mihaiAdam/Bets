using FluentMigrator;

namespace DataAccess.Migrations
{
    [Migration(202407156)]
    public class Migration_202407156_Add_Bets : Migration
    {
        public override void Up()
        {
            Create.Table("Bets")
            .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("Name").AsString(255)
            .WithColumn("Date").AsDateTime()
            .WithColumn("BetableEntityA").AsGuid()
            .WithColumn("BetableEntityB").AsGuid();
        }

        public override void Down()
        {
            Delete.Table("Bets");
        }
    }
}
