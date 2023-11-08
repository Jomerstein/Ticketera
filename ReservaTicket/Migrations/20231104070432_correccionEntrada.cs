using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservaTicket.Migrations
{
    /// <inheritdoc />
    public partial class correccionEntrada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "espectaculo",
                newName: "idEspectaculo");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "entrada",
                newName: "idEntrada");

            migrationBuilder.AddColumn<string>(
                name: "codigoEntrada",
                table: "entrada",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "codigoEntrada",
                table: "entrada");

            migrationBuilder.RenameColumn(
                name: "idEspectaculo",
                table: "espectaculo",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "idEntrada",
                table: "entrada",
                newName: "id");
        }
    }
}
