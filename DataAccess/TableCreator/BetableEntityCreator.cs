using FluentMigrator;

namespace DataAccess.TableCreator
{
    [Migration(202407154)]
    public class BetableEntityCreator : Migration
    {
        public override void Down()
        {
            Delete.Table("BetableEntity");
        }

        public override void Up()
        {
            Console.WriteLine("Betable Entity Up");

            if (!Schema.Table("BetableEntity").Exists())
            {
                Create.Table("BetableEntity")
                .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                .WithColumn("Name").AsString();
            }
        }
    }
}
