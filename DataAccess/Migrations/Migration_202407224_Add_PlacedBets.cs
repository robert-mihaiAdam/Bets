using FluentMigrator;

namespace DataAccess.Migrations
{
    [Migration(202407224)]
    public class Migration_202407224_Add_PlacedBets : Migration
    {
        public override void Up()
        {
            Create.Table("PlacedBets")
            .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("UserId").AsGuid()
            .WithColumn("PlacedDate").AsDateTime()
            .WithColumn("Type").AsString(1).NotNullable()
            .WithColumn("QuoteId").AsGuid();
        }

        public override void Down()
        {
            Delete.Table("PlacedBets");
        }
    }
}
