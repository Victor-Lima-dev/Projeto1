using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResolverQuestao.Migrations
{
    /// <inheritdoc />
    public partial class Novaspropriedadesnamodeldefeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avaliacao",
                table: "FeedBacks",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "Avaliada",
                table: "FeedBacks",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Resolvida",
                table: "FeedBacks",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Solucao",
                table: "FeedBacks",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avaliacao",
                table: "FeedBacks");

            migrationBuilder.DropColumn(
                name: "Avaliada",
                table: "FeedBacks");

            migrationBuilder.DropColumn(
                name: "Resolvida",
                table: "FeedBacks");

            migrationBuilder.DropColumn(
                name: "Solucao",
                table: "FeedBacks");
        }
    }
}
