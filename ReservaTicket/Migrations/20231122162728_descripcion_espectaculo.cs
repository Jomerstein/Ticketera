using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservaTicket.Migrations
{
    /// <inheritdoc />
    public partial class descripcion_espectaculo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "descripcionEspectaculo",
                table: "espectaculo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "descripcionEspectaculo",
                table: "espectaculo");
        }
    }
}
