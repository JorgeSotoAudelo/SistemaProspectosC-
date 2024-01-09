using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaProspectosC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prospectos",
                columns: table => new
                {
                    prospectoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nombre = table.Column<string>(type: "TEXT", nullable: false),
                    primerApellido = table.Column<string>(type: "TEXT", nullable: false),
                    segundoApellido = table.Column<string>(type: "TEXT", nullable: false),
                    calle = table.Column<string>(type: "TEXT", nullable: false),
                    numero = table.Column<string>(type: "TEXT", nullable: false),
                    colonia = table.Column<string>(type: "TEXT", nullable: false),
                    codigoPostal = table.Column<string>(type: "TEXT", nullable: false),
                    telefono = table.Column<string>(type: "TEXT", nullable: false),
                    RFC = table.Column<string>(type: "TEXT", nullable: false),
                    estatus = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prospectos", x => x.prospectoId);
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    documentoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    titulo = table.Column<string>(type: "TEXT", nullable: false),
                    ruta = table.Column<string>(type: "TEXT", nullable: false),
                    prospectoID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.documentoId);
                    table.ForeignKey(
                        name: "FK_Documentos_Prospectos_prospectoID",
                        column: x => x.prospectoID,
                        principalTable: "Prospectos",
                        principalColumn: "prospectoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_prospectoID",
                table: "Documentos",
                column: "prospectoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "Prospectos");
        }
    }
}
