using FluentMigrator;

namespace DataAccess.Migrations
{
    [Migration(202407294)]
    public class Migration_202407294_PlacedBets_AddBetPrice : Migration
    {
        public override void Up()
        {
            Alter.Table("PlacedBets")
                .AddColumn("BetPrice")
                .AsDecimal()
                .WithDefaultValue(1);
        }

        public override void Down()
        {
            Delete.Column("BetPrice").FromTable("PlacedBets");
        }
    }
}
