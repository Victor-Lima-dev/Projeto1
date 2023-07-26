using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResolverQuestao.Migrations
{
    /// <inheritdoc />
    public partial class ListaExerciciosadicionandoatabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExercicioListaExercicio_ListaExercicio_ListaExerciciosListaE~",
                table: "ExercicioListaExercicio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListaExercicio",
                table: "ListaExercicio");

            migrationBuilder.RenameTable(
                name: "ListaExercicio",
                newName: "ListaExercicios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListaExercicios",
                table: "ListaExercicios",
                column: "ListaExercicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExercicioListaExercicio_ListaExercicios_ListaExerciciosLista~",
                table: "ExercicioListaExercicio",
                column: "ListaExerciciosListaExercicioId",
                principalTable: "ListaExercicios",
                principalColumn: "ListaExercicioId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExercicioListaExercicio_ListaExercicios_ListaExerciciosLista~",
                table: "ExercicioListaExercicio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListaExercicios",
                table: "ListaExercicios");

            migrationBuilder.RenameTable(
                name: "ListaExercicios",
                newName: "ListaExercicio");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListaExercicio",
                table: "ListaExercicio",
                column: "ListaExercicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExercicioListaExercicio_ListaExercicio_ListaExerciciosListaE~",
                table: "ExercicioListaExercicio",
                column: "ListaExerciciosListaExercicioId",
                principalTable: "ListaExercicio",
                principalColumn: "ListaExercicioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
