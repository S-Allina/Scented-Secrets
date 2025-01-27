using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shop.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CtegoryName",
                table: "Products",
                newName: "CategoryName");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "ProductDescription", "ProductImg", "ProductName", "ProductPrice" },
                values: new object[,]
                {
                    { 1, "Мужской парфюм", "Магнетический аромат со свежими нотами", "https://vodar.by/media/catalog/product/cache/1/small_image/165x/9df78eab33525d08d6e5fb8d27136e95/1/3/1349097256_1_.jpg", "Lacoste Eau De Lacoste L.12.12. Blanc", 760.0 },
                    { 3, "Женский парфюм", "Изысканный женский аромат с нотами цветов и цитрусовых", "https://vodar.by/media/catalog/product/cache/1/small_image/165x/9df78eab33525d08d6e5fb8d27136e95/1/4/147565_1_.jpg", "Chanel Coco Mademoiselle", 900.0 },
                    { 4, "Женский парфюм", "Свежий и яркий аромат для женщин", "https://vodar.by/media/catalog/product/cache/1/small_image/165x/9df78eab33525d08d6e5fb8d27136e95/d/o/dolce___gabbana_dolce_peony-_1_1__1_.jpg", "Dolce&Gabbana Dolce Peonye", 950.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Products",
                newName: "CtegoryName");
        }
    }
}
