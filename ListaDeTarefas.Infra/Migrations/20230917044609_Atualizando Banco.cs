using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ListaDeTarefas.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoAlteracaoSenha",
                table: "Usuario",
                type: "varchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CodigoVerificacaoEmail",
                table: "Usuario",
                type: "varchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataExpiracaoCodigoEmail",
                table: "Usuario",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataVerificacaoCodigoEmail",
                table: "Usuario",
                type: "datetime",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoAlteracaoSenha",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "CodigoVerificacaoEmail",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "DataExpiracaoCodigoEmail",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "DataVerificacaoCodigoEmail",
                table: "Usuario");
        }
    }
}
