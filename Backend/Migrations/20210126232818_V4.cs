using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class V4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem__Cart_cartID",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "BasePrice",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CartItem");

            migrationBuilder.RenameColumn(
                name: "cartID",
                table: "CartItem",
                newName: "CartID");

            migrationBuilder.RenameColumn(
                name: "Qunatity",
                table: "CartItem",
                newName: "Quantity");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_cartID",
                table: "CartItem",
                newName: "IX_CartItem_CartID");

            migrationBuilder.AddColumn<int>(
                name: "productID",
                table: "CartItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_productID",
                table: "CartItem",
                column: "productID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem__Cart_CartID",
                table: "CartItem",
                column: "CartID",
                principalTable: "_Cart",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Products_productID",
                table: "CartItem",
                column: "productID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem__Cart_CartID",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Products_productID",
                table: "CartItem");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_productID",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "productID",
                table: "CartItem");

            migrationBuilder.RenameColumn(
                name: "CartID",
                table: "CartItem",
                newName: "cartID");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "CartItem",
                newName: "Qunatity");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_CartID",
                table: "CartItem",
                newName: "IX_CartItem_cartID");

            migrationBuilder.AddColumn<double>(
                name: "BasePrice",
                table: "CartItem",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CartItem",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem__Cart_cartID",
                table: "CartItem",
                column: "cartID",
                principalTable: "_Cart",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
