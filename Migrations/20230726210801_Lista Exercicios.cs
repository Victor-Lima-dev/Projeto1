using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResolverQuestao.Migrations
{
    /// <inheritdoc />
    public partial class ListaExercicios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MaterialSuporte",
                table: "Exercicios",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Explicacao",
                table: "Exercicios",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ListaExercicio",
                columns: table => new
                {
                    ListaExercicioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaterialSuporte = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaExercicio", x => x.ListaExercicioId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ExercicioListaExercicio",
                columns: table => new
                {
                    ExerciciosExercicioId = table.Column<int>(type: "int", nullable: false),
                    ListaExerciciosListaExercicioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercicioListaExercicio", x => new { x.ExerciciosExercicioId, x.ListaExerciciosListaExercicioId });
                    table.ForeignKey(
                        name: "FK_ExercicioListaExercicio_Exercicios_ExerciciosExercicioId",
                        column: x => x.ExerciciosExercicioId,
                        principalTable: "Exercicios",
                        principalColumn: "ExercicioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExercicioListaExercicio_ListaExercicio_ListaExerciciosListaE~",
                        column: x => x.ListaExerciciosListaExercicioId,
                        principalTable: "ListaExercicio",
                        principalColumn: "ListaExercicioId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ExercicioListaExercicio_ListaExerciciosListaExercicioId",
                table: "ExercicioListaExercicio",
                column: "ListaExerciciosListaExercicioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExercicioListaExercicio");

            migrationBuilder.DropTable(
                name: "ListaExercicio");

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "MaterialSuporte",
                keyValue: null,
                column: "MaterialSuporte",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "MaterialSuporte",
                table: "Exercicios",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Exercicios",
                keyColumn: "Explicacao",
                keyValue: null,
                column: "Explicacao",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Explicacao",
                table: "Exercicios",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
