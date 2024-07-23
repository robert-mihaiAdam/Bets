using FluentMigrator;

namespace DataAccess.Migrations
{
    [Migration(202407221)]
    public class Migration_202407221_Add_Entities : Migration
    {
        public override void Up()
        {
            Alter.Table("BetableEntity")
                 .AlterColumn("Name")
                 .AsString(255).WithDefaultValue("");
        }

        public override void Down()
        {
            Alter.Table("BetableEntity")
                  .AlterColumn("Name")
                  .AsString(255);
        }
    }
}
