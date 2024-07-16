using FluentMigrator;

namespace DataAccess.TableCreator
{
    [Migration(202407157)]
    public class PlacedBetCreator : Migration
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
