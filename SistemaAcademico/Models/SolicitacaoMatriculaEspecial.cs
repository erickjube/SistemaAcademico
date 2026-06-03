using SistemaAcademico.ENUMs;

namespace SistemaAcademico.Models;

public class SolicitacaoMatriculaEspecial
{
    public int Id { get; private set; }
    public string Justificativa { get; private set; } = string.Empty;
    public DateTime DataSolicitacao { get; private set; } = DateTime.UtcNow;
    public SolicitacaoStatus Status { get; private set; } = SolicitacaoStatus.Pendente;

    public int AlunoId { get; private set; }
    public Usuario? Aluno { get; private set; }

    public int TurmaId { get; private set; }
    public Turma? Turma { get; private set; } 

    public SolicitacaoMatriculaEspecial() { }

    public SolicitacaoMatriculaEspecial(int alunoId, int turmaId, string justificativa)
    {
        if (alunoId <= 0) throw new ArgumentException("AlunoId deve ser um número positivo.", nameof(alunoId));
        if (turmaId <= 0) throw new ArgumentException("TurmaId deve ser um número positivo.", nameof(turmaId));
        if (string.IsNullOrWhiteSpace(justificativa)) throw new ArgumentException("Justificativa obrigatória.", nameof(justificativa));
        AlunoId = alunoId;
        TurmaId = turmaId;
        Justificativa = justificativa;
        DataSolicitacao = DateTime.UtcNow;
        Status = SolicitacaoStatus.Pendente;
    }

    public void Aprovar()
    {
        if (Status != SolicitacaoStatus.Pendente)
            throw new InvalidOperationException("A solicitação já foi processada.");
        Status = SolicitacaoStatus.Aprovada;
    }

    public void Rejeitar()
    {
        if (Status != SolicitacaoStatus.Pendente)
            throw new InvalidOperationException("A solicitação já foi processada.");
        Status = SolicitacaoStatus.Rejeitada;
    }
}
