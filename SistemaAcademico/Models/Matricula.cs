using SistemaAcademico.ENUMs;

namespace SistemaAcademico.Models;

public class Matricula
{
    public int Id { get; private set; }

    public int AlunoId { get; private set; }
    public Usuario? Aluno { get; private set; }

    public int TurmaId { get; private set; }
    public Turma? Turma { get; private   set; }

    public MatriculaStatus Status { get; private set; } = MatriculaStatus.Ativa;
    public DateTime DataMatricula { get; private set; } = DateTime.UtcNow;

    public string? Justificativa { get; private set; }

    public Nota? Nota { get; private set; }

    public Matricula() { }

    public Matricula(int alunoId, int turmaId)
    {
        if (alunoId <= 0) throw new ArgumentException("AlunoId deve ser um número positivo.", nameof(AlunoId));
        if (turmaId <= 0) throw new ArgumentException("TurmaId deve ser um número positivo.", nameof(TurmaId));
        AlunoId = alunoId;
        TurmaId = turmaId;
    }

    public void Cancelar()
    {
        if (Status == MatriculaStatus.Cancelada) throw new InvalidOperationException("A matrícula já está cancelada.");        
        Status = MatriculaStatus.Cancelada;
    }

    public void TornarEspecial(string justificativa)
    {
        if (string.IsNullOrWhiteSpace(justificativa)) throw new ArgumentException("Justificativa obrigatória.");
        if (Status == MatriculaStatus.Especial) throw new InvalidOperationException("A matrícula já é especial.");
        Justificativa = justificativa;
        Status = MatriculaStatus.Especial;
    }
}
