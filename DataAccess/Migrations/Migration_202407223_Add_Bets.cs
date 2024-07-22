using FluentMigrator;

namespace DataAccess.Migrations
{
    [Migration(202407223)]
    public class Migration_202407223_Add_Bets : Migration
    {
        public override void Up()
        {
            Create.Table("Bets")
            .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("Name").AsString(255).NotNullable()
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
