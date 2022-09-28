using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tkytekstil.DAL.Migrations
{
    public partial class v14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_size_SizeId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_SizeId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "products");

            migrationBuilder.CreateTable(
                name: "sizeNumProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sizeNumProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sizeNumProduct_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sizeNumProduct_size_SizeId",
                        column: x => x.SizeId,
                        principalTable: "size",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sizeNumProduct_ProductId",
                table: "sizeNumProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_sizeNumProduct_SizeId",
                table: "sizeNumProduct",
                column: "SizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sizeNumProduct");

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_products_SizeId",
                table: "products",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_size_SizeId",
                table: "products",
                column: "SizeId",
                principalTable: "size",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
