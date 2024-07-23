using FluentMigrator;

namespace DataAccess.Migrations.Old
{
    [Migration(202407157)]
    public class Migration_202407157_Add_PlacedBets : Migration
    {
        public override void Up()
        {
            Create.Table("PlacedBets")
            .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("UserId").AsGuid()
            .WithColumn("PlacedDate").AsDateTime()
            .WithColumn("Type").AsInt32()
            .WithColumn("QuoteId").AsGuid();
        }

        public override void Down()
        {
            Delete.Table("PlacedBets");
        }
    }
}
