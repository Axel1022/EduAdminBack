using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduAdmin.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdmitidoComo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmitidoComo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carrera",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrera", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estudiante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCarrera = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarreraId = table.Column<int>(type: "int", nullable: true),
                    IdAdmitidoComo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdmitidoComoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estudiante_AdmitidoComo_AdmitidoComoId",
                        column: x => x.AdmitidoComoId,
                        principalTable: "AdmitidoComo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Estudiante_Carrera_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carrera",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estudiante_AdmitidoComoId",
                table: "Estudiante",
                column: "AdmitidoComoId");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiante_CarreraId",
                table: "Estudiante",
                column: "CarreraId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estudiante");

            migrationBuilder.DropTable(
                name: "AdmitidoComo");

            migrationBuilder.DropTable(
                name: "Carrera");
        }
    }
}
