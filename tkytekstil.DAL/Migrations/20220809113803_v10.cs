using Microsoft.EntityFrameworkCore.Migrations;

namespace tkytekstil.DAL.Migrations
{
    public partial class v10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShopperPassword",
                table: "Shoppers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShopperUserName",
                table: "Shoppers",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShopperPassword",
                table: "Shoppers");

            migrationBuilder.DropColumn(
                name: "ShopperUserName",
                table: "Shoppers");
        }
    }
}
