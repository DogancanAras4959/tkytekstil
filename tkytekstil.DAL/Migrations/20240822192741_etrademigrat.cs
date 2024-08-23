using Microsoft.EntityFrameworkCore.Migrations;

namespace tkytekstil.DAL.Migrations
{
    public partial class etrademigrat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShopperIdentityNumber",
                table: "Shoppers",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShopperSurname",
                table: "Shoppers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "products",
                type: "decimal(18,2)",
                maxLength: 15,
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShopperIdentityNumber",
                table: "Shoppers");

            migrationBuilder.DropColumn(
                name: "ShopperSurname",
                table: "Shoppers");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "products");
        }
    }
}
