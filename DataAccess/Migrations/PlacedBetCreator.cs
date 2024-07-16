using FluentMigrator;

namespace DataAccess.Migrations
{
    [Migration(202407157)]
    public class Migration_202407157_Add_PlacedBets : Migration
    {
        public override void Down()
        {
            Delete.Table("PlacedBets");
        }

        public override void Up()
        {
            if (!Schema.Table("PlacedBets").Exists())
            {
                Create.Table("PlacedBets")
                 .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                 .WithColumn("UserId ").AsGuid()
                 .WithColumn("PlacedDate").AsDateTime().WithDefaultValue(DateTime.Now)
                 .WithColumn("Type").AsString()
                 .WithColumn("QuoteId").AsGuid();
            }
        }
    }
}
