using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace RunMigration.Migrations
{
    [Migration(202011281345)]
    public class Init : Migration
    {
        public override void Up()
        {
            Create.Table("Storage").WithDescription("A place where money is located (pocket, bill, etc)")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString(150).NotNullable()
                .WithColumn("Disabled").AsBoolean().NotNullable().Indexed()
                .WithColumn("DisableDate").AsDateTime().Nullable();
            Create.UniqueConstraint("UK_Storage").OnTable("Storage")
                .Columns("Name", "DisableDate");

            Create.Table("Purpose").WithDescription("A target for money is needed (car, house, etc)")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString(150).NotNullable()
                .WithColumn("Disabled").AsBoolean().NotNullable().Indexed()
                .WithColumn("DisableDate").AsDateTime().Nullable();
            Create.UniqueConstraint("UK_Purpose").OnTable("Purpose")
                .Columns("Name", "DisableDate");

            Create.Table("StoreItem").WithDescription("Summ of money for the target on the store")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("StorageId").AsInt64().NotNullable().ForeignKey("FK_StoreItem_Storage", "Storage", "Id")
                .WithColumn("PurposeId").AsInt64().NotNullable().ForeignKey("FK_StoreItem_Purpose", "Purpose", "Id")
                .WithColumn("Quantity").AsDouble().NotNullable()
                .WithColumn("Disabled").AsBoolean().NotNullable().Indexed()
                .WithColumn("DisableDate").AsDateTime().Nullable();
            Create.UniqueConstraint("UK_StoreItem").OnTable("StoreItem")
                .Columns("StorageId", "PurposeId", "DisableDate");

            Create.Table("Movement").WithDescription("History of operations")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("SourceStoreItemId").AsInt64().Nullable().ForeignKey("FK_Movement_SourceStoreItem", "StoreItem", "Id")
                .WithColumn("DestinationStoreItemId").AsInt64().Nullable().ForeignKey("FK_Movement_DestinationStoreItem", "StoreItem", "Id")
                .WithColumn("Quantity").AsDouble().NotNullable()
                .WithColumn("Comment").AsString(1000).Nullable()
                .WithColumn("MoveDate").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.UniqueConstraint("UK_StoreItem");
            Delete.UniqueConstraint("UK_Purpose");
            Delete.UniqueConstraint("UK_Storage");

            Delete.Table("Movement");
            Delete.Table("StoreItem");
            Delete.Table("Purpose");
            Delete.Table("Storage");
        }
    }
}
