using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    public partial class PopularCategorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categorias(Nome, ImagemURL) Values('Bebidas', 'bebidas.jpg')");
            migrationBuilder.Sql("INSERT INTO Categorias(Nome, ImagemURL) Values('Lanches', 'lanches.jpg')");
            migrationBuilder.Sql("INSERT INTO Categorias(Nome, ImagemURL) Values('Sobremesas', 'sobremesas.jpg')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
        }
    }
}
