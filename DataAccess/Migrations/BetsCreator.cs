using FluentMigrator;

namespace DataAccess.Migrations
{
    [Migration(202407156)]
    public class Migration_202407156_Add_Bets : Migration
    {
        public override void Down()
        {
            Delete.Table("Bets");
        }

        public override void Up()
        {
            if (!Schema.Table("Bets").Exists())
            {
                Create.Table("Bets")
                .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                .WithColumn("Name").AsString()
                .WithColumn("Date").AsDateTime().WithDefaultValue(DateTime.Now)
                .WithColumn("BetableEntityA").AsGuid()
                .WithColumn("BetableEntityB").AsGuid();
            }
        }
    }
}
