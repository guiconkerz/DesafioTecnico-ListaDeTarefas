using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ListaDeTarefas.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Add_VerificaAlteracao_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataVerificacaoCodigoSenha",
                table: "Usuario",
                type: "datetime",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataVerificacaoCodigoSenha",
                table: "Usuario");
        }
    }
}
