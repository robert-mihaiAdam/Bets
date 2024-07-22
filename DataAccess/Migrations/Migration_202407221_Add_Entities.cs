using FluentMigrator;

namespace DataAccess.Migrations
{
    [Migration(202407221)]
    public class Migration_202407221_Add_Entities : Migration
    {
        public override void Up()
        {
            Create.Table("BetableEntity")
                .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                .WithColumn("Name").AsString(255).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("BetableEntity");
        }
    }
}
