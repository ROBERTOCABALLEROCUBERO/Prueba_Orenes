using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orenes.Migrations
{
    /// <inheritdoc />
    public partial class NuevaMigracion23321 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehiculoId1",
                table: "Pedidos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_VehiculoId1",
                table: "Pedidos",
                column: "VehiculoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Vehiculos_VehiculoId1",
                table: "Pedidos",
                column: "VehiculoId1",
                principalTable: "Vehiculos",
                principalColumn: "VehiculoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Vehiculos_VehiculoId1",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_VehiculoId1",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "VehiculoId1",
                table: "Pedidos");
        }
    }
}
