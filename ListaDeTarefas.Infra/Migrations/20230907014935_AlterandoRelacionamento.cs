using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ListaDeTarefas.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "FkTarefa",
                table: "Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_FkUsuario",
                table: "Tarefa",
                column: "FkUsuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tarefa_FkUsuario",
                table: "Tarefa");

            migrationBuilder.AddColumn<int>(
                name: "FkTarefa",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_FkUsuario",
                table: "Tarefa",
                column: "FkUsuario");
        }
    }
}
