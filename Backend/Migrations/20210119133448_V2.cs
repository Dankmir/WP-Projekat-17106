using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Products_ProductID",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Restaurants_RestaurantID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_ProductID",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "CartItem");

            migrationBuilder.RenameColumn(
                name: "RestaurantID",
                table: "Products",
                newName: "restaurantID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_RestaurantID",
                table: "Products",
                newName: "IX_Products_restaurantID");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Restaurants",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Restaurants",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Restaurants_restaurantID",
                table: "Products",
                column: "restaurantID",
                principalTable: "Restaurants",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Restaurants_restaurantID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Restaurants");

            migrationBuilder.RenameColumn(
                name: "restaurantID",
                table: "Products",
                newName: "RestaurantID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_restaurantID",
                table: "Products",
                newName: "IX_Products_RestaurantID");

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "CartItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_ProductID",
                table: "CartItem",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Products_ProductID",
                table: "CartItem",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Restaurants_RestaurantID",
                table: "Products",
                column: "RestaurantID",
                principalTable: "Restaurants",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
