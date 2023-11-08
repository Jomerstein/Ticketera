using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservaTicket.Migrations
{
    /// <inheritdoc />
    public partial class migracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NomUsuario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    contrasenia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "espectaculo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuarioID = table.Column<int>(type: "int", nullable: false),
                    fechaEspectaculo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cantEntradas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_espectaculo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_espectaculo_usuarios_usuarioID",
                        column: x => x.usuarioID,
                        principalTable: "usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "entrada",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idEspectaculo = table.Column<int>(type: "int", nullable: false),
                    estaUsada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entrada", x => x.id);
                    table.ForeignKey(
                        name: "FK_entrada_espectaculo_idEspectaculo",
                        column: x => x.idEspectaculo,
                        principalTable: "espectaculo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_entrada_idEspectaculo",
                table: "entrada",
                column: "idEspectaculo");

            migrationBuilder.CreateIndex(
                name: "IX_espectaculo_usuarioID",
                table: "espectaculo",
                column: "usuarioID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "entrada");

            migrationBuilder.DropTable(
                name: "espectaculo");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
