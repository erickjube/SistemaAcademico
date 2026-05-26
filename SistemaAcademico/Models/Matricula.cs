using SistemaAcademico.ENUMs;

namespace SistemaAcademico.Models;

public class Matricula
{
    public int Id { get; set; }

    public int AlunoId { get; set; }
    public Usuario? Aluno { get; set; }

    public int TurmaId { get; set; }
    public Turma? Turma { get; set; }

    public MatriculaStatus Status { get; set; } = MatriculaStatus.Ativa;
    public DateTime DataMatricula { get; set; } = DateTime.UtcNow;

    public string? Justificativa { get; set; }

    public Nota? Nota { get; set; }
}
