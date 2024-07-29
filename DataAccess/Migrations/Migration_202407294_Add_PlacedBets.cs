using FluentMigrator;

namespace DataAccess.Migrations
{
    [Migration(202407294)]
    public class Migration_202407294_Add_PlacedBets : Migration
    {
        public override void Up()
        {
            Alter.Table("PlacedBets")
                .AddColumn("BetPrice")
                .AsInt64()
                .WithDefaultValue(1);
        }

        public override void Down()
        {
            Delete.Column("BetPrice").FromTable("PlacedBets");
        }
    }
}
