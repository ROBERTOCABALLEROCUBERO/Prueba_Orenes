using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orenes.Migrations
{
    /// <inheritdoc />
    public partial class CambiosLogica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detalles",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Detalles",
                table: "PedidoEntregado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Detalles",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Detalles",
                table: "PedidoEntregado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
