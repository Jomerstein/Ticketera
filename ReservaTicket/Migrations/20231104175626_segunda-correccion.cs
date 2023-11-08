using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservaTicket.Migrations
{
    /// <inheritdoc />
    public partial class segundacorreccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "estaVendida",
                table: "entrada",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "estaVendida",
                table: "entrada");
        }
    }
}
