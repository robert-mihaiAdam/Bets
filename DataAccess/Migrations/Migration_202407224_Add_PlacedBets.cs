using FluentMigrator;

namespace DataAccess.Migrations
{
    [Migration(202407224)]
    public class Migration_202407224_Add_PlacedBets : Migration
    {
        public override void Up()
        {
            Alter.Table("PlacedBets")
                .AlterColumn("UserId").AsGuid().WithDefaultValue(Guid.Empty);

            Alter.Table("PlacedBets")
                 .AlterColumn("Type").AsInt32().WithDefaultValue(0);;
        }

        public override void Down()
        {
            Alter.Table("PlacedBets")
                 .AlterColumn("UserId").AsGuid();

            Alter.Table("PlacedBets")
                 .AlterColumn("Type").AsInt32();

            Alter.Table("PlacedBets")
                 .AlterColumn("QuoteId").AsGuid();
        }
    }
}
