using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class UpdatedPurchasedItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedItem_Accounts_AccountId",
                table: "PurchasedItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchasedItem",
                table: "PurchasedItem");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "PurchasedItem");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "PurchasedItem");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PurchasedItem");

            migrationBuilder.RenameTable(
                name: "PurchasedItem",
                newName: "PurchasedItems");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasedItem_AccountId",
                table: "PurchasedItems",
                newName: "IX_PurchasedItems_AccountId");

            migrationBuilder.AddColumn<string>(
                name: "AppId",
                table: "PurchasedItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssetId",
                table: "PurchasedItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClassId",
                table: "PurchasedItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContextId",
                table: "PurchasedItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstanceId",
                table: "PurchasedItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemId",
                table: "PurchasedItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarketHashName",
                table: "PurchasedItems",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchasedAt",
                table: "PurchasedItems",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "WithdrawableAt",
                table: "PurchasedItems",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasedItems",
                table: "PurchasedItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedItems_Accounts_AccountId",
                table: "PurchasedItems",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedItems_Accounts_AccountId",
                table: "PurchasedItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchasedItems",
                table: "PurchasedItems");

            migrationBuilder.DropColumn(
                name: "AppId",
                table: "PurchasedItems");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "PurchasedItems");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "PurchasedItems");

            migrationBuilder.DropColumn(
                name: "ContextId",
                table: "PurchasedItems");

            migrationBuilder.DropColumn(
                name: "InstanceId",
                table: "PurchasedItems");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "PurchasedItems");

            migrationBuilder.DropColumn(
                name: "MarketHashName",
                table: "PurchasedItems");

            migrationBuilder.DropColumn(
                name: "PurchasedAt",
                table: "PurchasedItems");

            migrationBuilder.DropColumn(
                name: "WithdrawableAt",
                table: "PurchasedItems");

            migrationBuilder.RenameTable(
                name: "PurchasedItems",
                newName: "PurchasedItem");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasedItems_AccountId",
                table: "PurchasedItem",
                newName: "IX_PurchasedItem_AccountId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "PurchasedItem",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "PurchasedItem",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PurchasedItem",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasedItem",
                table: "PurchasedItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedItem_Accounts_AccountId",
                table: "PurchasedItem",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
