﻿using FluentMigrator;

namespace DataAccess.Migrations
{
    [Migration(202407224)]
    public class Migration_202407224_Add_PlacedBets : Migration
    {
        public override void Up()
        {
            Create.Table("PlacedBets")
            .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("UserId").AsGuid().NotNullable()
            .WithColumn("PlacedDate").AsDateTime()
            .WithColumn("Type").AsInt32().NotNullable()
            .WithColumn("QuoteId").AsGuid().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("PlacedBets");
        }
    }
}
