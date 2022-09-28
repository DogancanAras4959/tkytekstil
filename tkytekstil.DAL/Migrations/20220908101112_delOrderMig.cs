using Microsoft.EntityFrameworkCore.Migrations;

namespace tkytekstil.DAL.Migrations
{
    public partial class delOrderMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "order");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "order");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "orderProduct",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "orderProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "orderProduct",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "orderProduct");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "orderProduct");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "orderProduct");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "order",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "order",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);
        }
    }
}
