using FluentMigrator;

namespace DataAccess.Migrations
{
    [Migration(202407223)]
    public class Migration_202407223_Add_Bets : Migration
    {
        public override void Up()
        {
            Alter.Table("Bets")
                .AlterColumn("Name").AsString(255).WithDefaultValue("");
        }

        public override void Down()
        {
            Alter.Table("Bets")
             .AlterColumn("Name").AsString(255);
        }
    }
}
