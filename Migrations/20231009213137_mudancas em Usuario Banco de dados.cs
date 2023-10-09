using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResolverQuestao.Migrations
{
    /// <inheritdoc />
    public partial class mudancasemUsuarioBancodedados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Objetivo",
                table: "UsuarioBancoDados");

            migrationBuilder.DropColumn(
                name: "Profissao",
                table: "UsuarioBancoDados");

            migrationBuilder.AddColumn<double>(
                name: "MetaMedia",
                table: "UsuarioBancoDados",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetaMedia",
                table: "UsuarioBancoDados");

            migrationBuilder.AddColumn<string>(
                name: "Objetivo",
                table: "UsuarioBancoDados",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Profissao",
                table: "UsuarioBancoDados",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
