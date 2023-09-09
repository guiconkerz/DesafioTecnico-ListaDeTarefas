using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ListaDeTarefas.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoRelacionamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefa_Usuario_UsuarioId",
                table: "Tarefa");

            migrationBuilder.DropIndex(
                name: "IX_Tarefa_FkUsuario",
                table: "Tarefa");

            migrationBuilder.DropIndex(
                name: "IX_Tarefa_UsuarioId",
                table: "Tarefa");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Tarefa");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_FkUsuario",
                table: "Tarefa",
                column: "FkUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tarefa_FkUsuario",
                table: "Tarefa");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Tarefa",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_FkUsuario",
                table: "Tarefa",
                column: "FkUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_UsuarioId",
                table: "Tarefa",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefa_Usuario_UsuarioId",
                table: "Tarefa",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "UsuarioId");
        }
    }
}
