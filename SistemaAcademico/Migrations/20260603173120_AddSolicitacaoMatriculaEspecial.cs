using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SistemaAcademico.Migrations
{
    /// <inheritdoc />
    public partial class AddSolicitacaoMatriculaEspecial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FimAulas",
                table: "PeriodosLetivos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "SolicitacoesMatriculaEspecial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Justificativa = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    DataSolicitacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    AlunoId = table.Column<int>(type: "integer", nullable: false),
                    TurmaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacoesMatriculaEspecial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitacoesMatriculaEspecial_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitacoesMatriculaEspecial_Usuarios_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacoesMatriculaEspecial_AlunoId",
                table: "SolicitacoesMatriculaEspecial",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacoesMatriculaEspecial_TurmaId",
                table: "SolicitacoesMatriculaEspecial",
                column: "TurmaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitacoesMatriculaEspecial");

            migrationBuilder.DropColumn(
                name: "FimAulas",
                table: "PeriodosLetivos");
        }
    }
}
