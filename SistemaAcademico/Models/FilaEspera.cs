namespace SistemaAcademico.Models;

public class FilaEspera
{
    public int Id { get; private set; }

    public int AlunoId { get; private set; }
    public Usuario? Aluno { get; private set; }

    public int TurmaId { get; private set; }
    public Turma? Turma { get; private set; }
    public DateTime DataEntrada { get; private set; } = DateTime.UtcNow;

    public FilaEspera() { }
    public FilaEspera(int alunoId, int turmaId)
    {
        if (alunoId <= 0) throw new ArgumentException("AlunoId deve ser um número positivo.", nameof(alunoId));
        if (turmaId <= 0) throw new ArgumentException("TurmaId deve ser um número positivo.", nameof(turmaId));
        AlunoId = alunoId;
        TurmaId = turmaId;
        DataEntrada = DateTime.UtcNow;
    }
}
