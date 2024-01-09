using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaProspectosC.Migrations
{
    /// <inheritdoc />
    public partial class observaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "observacion",
                table: "Prospectos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "observacion",
                table: "Prospectos");
        }
    }
}
