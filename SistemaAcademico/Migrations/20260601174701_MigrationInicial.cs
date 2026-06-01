using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SistemaAcademico.Migrations
{
    /// <inheritdoc />
    public partial class MigrationInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    MaxDisciplinas = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeriodosLetivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Semestre = table.Column<string>(type: "text", nullable: false),
                    InicioMatricula = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FimMatricula = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    InicioAulas = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodosLetivos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Telefone = table.Column<string>(type: "text", nullable: false),
                    Cpf = table.Column<string>(type: "text", nullable: false),
                    SenhaHash = table.Column<string>(type: "text", nullable: false),
                    Perfil = table.Column<string>(type: "text", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Disciplinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    CursoId = table.Column<int>(type: "integer", nullable: false),
                    PreRequisitoId = table.Column<int>(type: "integer", nullable: true),
                    CargaHoraria = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Disciplinas_PreRequisitoId",
                        column: x => x.PreRequisitoId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentosGerados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    HashSha256 = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CaminhoPdf = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    GeradoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentosGerados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentosGerados_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notificacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Mensagem = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Lida = table.Column<bool>(type: "boolean", nullable: false),
                    CriadaEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificacoes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DisciplinaId = table.Column<int>(type: "integer", nullable: false),
                    ProfessorId = table.Column<int>(type: "integer", nullable: false),
                    PeriodoLetivoId = table.Column<int>(type: "integer", nullable: false),
                    Vagas = table.Column<int>(type: "integer", nullable: false),
                    Fechada = table.Column<bool>(type: "boolean", nullable: false),
                    FormulaMediaTipo = table.Column<int>(type: "integer", nullable: false),
                    DataFechamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turmas_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Turmas_PeriodosLetivos_PeriodoLetivoId",
                        column: x => x.PeriodoLetivoId,
                        principalTable: "PeriodosLetivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Turmas_Usuarios_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FilaEsperas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AlunoId = table.Column<int>(type: "integer", nullable: false),
                    TurmaId = table.Column<int>(type: "integer", nullable: false),
                    DataEntrada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilaEsperas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilaEsperas_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FilaEsperas_Usuarios_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Matriculas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AlunoId = table.Column<int>(type: "integer", nullable: false),
                    TurmaId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", maxLength: 50, nullable: false),
                    DataMatricula = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Justificativa = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matriculas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matriculas_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matriculas_Usuarios_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MatriculaId = table.Column<int>(type: "integer", nullable: false),
                    P1 = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    P2 = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    Trabalho = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    Media = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    Situacao = table.Column<int>(type: "integer", maxLength: 50, nullable: false),
                    Frequencia = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notas_Matriculas_MatriculaId",
                        column: x => x.MatriculaId,
                        principalTable: "Matriculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_CursoId",
                table: "Disciplinas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_PreRequisitoId",
                table: "Disciplinas",
                column: "PreRequisitoId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosGerados_UsuarioId",
                table: "DocumentosGerados",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_FilaEsperas_AlunoId",
                table: "FilaEsperas",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_FilaEsperas_TurmaId",
                table: "FilaEsperas",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_AlunoId_TurmaId",
                table: "Matriculas",
                columns: new[] { "AlunoId", "TurmaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_TurmaId",
                table: "Matriculas",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_MatriculaId",
                table: "Notas",
                column: "MatriculaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notificacoes_UsuarioId",
                table: "Notificacoes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_DisciplinaId",
                table: "Turmas",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_PeriodoLetivoId",
                table: "Turmas",
                column: "PeriodoLetivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_ProfessorId",
                table: "Turmas",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentosGerados");

            migrationBuilder.DropTable(
                name: "FilaEsperas");

            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropTable(
                name: "Notificacoes");

            migrationBuilder.DropTable(
                name: "Matriculas");

            migrationBuilder.DropTable(
                name: "Turmas");

            migrationBuilder.DropTable(
                name: "Disciplinas");

            migrationBuilder.DropTable(
                name: "PeriodosLetivos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Cursos");
        }
    }
}
