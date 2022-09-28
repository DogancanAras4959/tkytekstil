using Microsoft.EntityFrameworkCore.Migrations;

namespace tkytekstil.DAL.Migrations
{
    public partial class v13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "products");

            migrationBuilder.DropColumn(
                name: "ProductDescription",
                table: "products");

            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "products");

            migrationBuilder.AddColumn<bool>(
                name: "Vitrin",
                table: "products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vitrin",
                table: "products");

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "products",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductDescription",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductPrice",
                table: "products",
                type: "decimal(18,2)",
                maxLength: 75,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
