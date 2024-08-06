using FluentMigrator;

namespace DataAccess.Migrations
{
    [Migration(202407154)]
    public class Migration_202407154_Add_Entities : Migration
    {
        public override void Up()
        {
            Console.WriteLine("O face vere");
            Create.Table("BetableEntity")
                .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                .WithColumn("Name").AsString(255);
        }

        public override void Down()
        {
            Delete.Table("BetableEntity");
        }
    }
}
