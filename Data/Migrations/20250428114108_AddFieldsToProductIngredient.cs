using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TequliasRestaurant.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToProductIngredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOptional",
                table: "ProductIngredients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "ProductIngredients",
                type: "decimal(18,3)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "ProductIngredients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ingredients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "ProductIngredients",
                keyColumns: new[] { "IngredientId", "ProductId" },
                keyValues: new object[] { 1, 1 },
                columns: new[] { "IsOptional", "Quantity", "Unit" },
                values: new object[] { false, null, null });

            migrationBuilder.UpdateData(
                table: "ProductIngredients",
                keyColumns: new[] { "IngredientId", "ProductId" },
                keyValues: new object[] { 4, 1 },
                columns: new[] { "IsOptional", "Quantity", "Unit" },
                values: new object[] { false, null, null });

            migrationBuilder.UpdateData(
                table: "ProductIngredients",
                keyColumns: new[] { "IngredientId", "ProductId" },
                keyValues: new object[] { 5, 1 },
                columns: new[] { "IsOptional", "Quantity", "Unit" },
                values: new object[] { false, null, null });

            migrationBuilder.UpdateData(
                table: "ProductIngredients",
                keyColumns: new[] { "IngredientId", "ProductId" },
                keyValues: new object[] { 6, 1 },
                columns: new[] { "IsOptional", "Quantity", "Unit" },
                values: new object[] { false, null, null });

            migrationBuilder.UpdateData(
                table: "ProductIngredients",
                keyColumns: new[] { "IngredientId", "ProductId" },
                keyValues: new object[] { 2, 2 },
                columns: new[] { "IsOptional", "Quantity", "Unit" },
                values: new object[] { false, null, null });

            migrationBuilder.UpdateData(
                table: "ProductIngredients",
                keyColumns: new[] { "IngredientId", "ProductId" },
                keyValues: new object[] { 4, 2 },
                columns: new[] { "IsOptional", "Quantity", "Unit" },
                values: new object[] { false, null, null });

            migrationBuilder.UpdateData(
                table: "ProductIngredients",
                keyColumns: new[] { "IngredientId", "ProductId" },
                keyValues: new object[] { 5, 2 },
                columns: new[] { "IsOptional", "Quantity", "Unit" },
                values: new object[] { false, null, null });

            migrationBuilder.UpdateData(
                table: "ProductIngredients",
                keyColumns: new[] { "IngredientId", "ProductId" },
                keyValues: new object[] { 6, 2 },
                columns: new[] { "IsOptional", "Quantity", "Unit" },
                values: new object[] { false, null, null });

            migrationBuilder.UpdateData(
                table: "ProductIngredients",
                keyColumns: new[] { "IngredientId", "ProductId" },
                keyValues: new object[] { 3, 3 },
                columns: new[] { "IsOptional", "Quantity", "Unit" },
                values: new object[] { false, null, null });

            migrationBuilder.UpdateData(
                table: "ProductIngredients",
                keyColumns: new[] { "IngredientId", "ProductId" },
                keyValues: new object[] { 4, 3 },
                columns: new[] { "IsOptional", "Quantity", "Unit" },
                values: new object[] { false, null, null });

            migrationBuilder.UpdateData(
                table: "ProductIngredients",
                keyColumns: new[] { "IngredientId", "ProductId" },
                keyValues: new object[] { 5, 3 },
                columns: new[] { "IsOptional", "Quantity", "Unit" },
                values: new object[] { false, null, null });

            migrationBuilder.UpdateData(
                table: "ProductIngredients",
                keyColumns: new[] { "IngredientId", "ProductId" },
                keyValues: new object[] { 6, 3 },
                columns: new[] { "IsOptional", "Quantity", "Unit" },
                values: new object[] { false, null, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://via.placeholder.com/150");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://via.placeholder.com/150");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://via.placeholder.com/150");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsOptional",
                table: "ProductIngredients");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductIngredients");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "ProductIngredients");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ImageUrl",
                value: "htpps://via.placeholder.com/150");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ImageUrl",
                value: "htpps://via.placeholder.com/150");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "ImageUrl",
                value: "htpps://via.placeholder.com/150");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
