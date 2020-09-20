using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace WebApi.Migrations
{
    public partial class CreatedPricesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ItemName = table.Column<string>(nullable: true),
                    AveragePrice = table.Column<decimal>(nullable: false),
                    MedianPrice = table.Column<decimal>(nullable: false),
                    StandardDeviation = table.Column<decimal>(nullable: false),
                    LowestPrice = table.Column<decimal>(nullable: false),
                    HighestPrice = table.Column<decimal>(nullable: false),
                    Sold = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prices_ItemName",
                table: "Prices",
                column: "ItemName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prices");
        }
    }
}
