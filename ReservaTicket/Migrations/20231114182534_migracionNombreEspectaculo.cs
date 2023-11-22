using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservaTicket.Migrations
{
    /// <inheritdoc />
    public partial class migracionNombreEspectaculo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "nombreEspectaculo",
                table: "espectaculo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nombreEspectaculo",
                table: "espectaculo");
        }
    }
}
