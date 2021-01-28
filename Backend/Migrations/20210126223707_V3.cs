using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem__Cart_CartID",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants__Cart__CartID",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants__CartID",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "_CartID",
                table: "Restaurants");

            migrationBuilder.RenameColumn(
                name: "CartID",
                table: "CartItem",
                newName: "cartID");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_CartID",
                table: "CartItem",
                newName: "IX_CartItem_cartID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem__Cart_cartID",
                table: "CartItem",
                column: "cartID",
                principalTable: "_Cart",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem__Cart_cartID",
                table: "CartItem");

            migrationBuilder.RenameColumn(
                name: "cartID",
                table: "CartItem",
                newName: "CartID");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_cartID",
                table: "CartItem",
                newName: "IX_CartItem_CartID");

            migrationBuilder.AddColumn<int>(
                name: "_CartID",
                table: "Restaurants",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants__CartID",
                table: "Restaurants",
                column: "_CartID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem__Cart_CartID",
                table: "CartItem",
                column: "CartID",
                principalTable: "_Cart",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants__Cart__CartID",
                table: "Restaurants",
                column: "_CartID",
                principalTable: "_Cart",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
