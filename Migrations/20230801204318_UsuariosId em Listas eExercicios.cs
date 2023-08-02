using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResolverQuestao.Migrations
{
    /// <inheritdoc />
    public partial class UsuariosIdemListaseExercicios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "ListaExercicios",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Exercicios",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "ListaExercicios");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Exercicios");
        }
    }
}
