using SistemaAcademico.ENUMs;

namespace SistemaAcademico.Models;

public class Turma
{
    public int Id { get; private set; }

    public int DisciplinaId { get; private set; }
    public Disciplina? Disciplina { get; private set; }

    public int ProfessorId { get; private set; }
    public Usuario? Professor { get; private set; }
    public int PeriodoLetivoId { get; private set; }
    public PeriodoLetivo PeriodoLetivo { get; private set; }

    public int Vagas { get; private set; }
    public bool Fechada { get; private set; }
    public FormulaMediaTipo FormulaMediaTipo { get; private set; } = FormulaMediaTipo.Ponderada;
    public DateTime? DataFechamento { get; private set; }

    public ICollection<Matricula> Matriculas { get; private set; } = new List<Matricula>();

    public Turma() { }
    public Turma(int disciplinaId, int professorId, int periodoLetivoId, int vagas, FormulaMediaTipo formulaMediaTipo)
    {
        if (disciplinaId == 0) throw new ArgumentException("DisciplinaId deve ser um número positivo.", nameof(disciplinaId));
        if (professorId == 0) throw new ArgumentException("ProfessorId deve ser um número positivo.", nameof(professorId));
        if (periodoLetivoId == 0) throw new ArgumentException("PeriodoLetivoId deve ser um número positivo.", nameof(periodoLetivoId));
        if (vagas <= 0) throw new ArgumentException("Vagas deve ser um número positivo.", nameof(vagas));
        DisciplinaId = disciplinaId;
        ProfessorId = professorId;
        PeriodoLetivoId = periodoLetivoId;
        Vagas = vagas;
        FormulaMediaTipo = formulaMediaTipo;
    }

    public void FecharDiario()
    {
        if (Fechada) throw new InvalidOperationException("O diário já está fechado.");
        Fechada = true;
        DataFechamento = DateTime.UtcNow;
    }
  
    public bool ValidarVagas()
    {
        var vagasDisponiveis = Matriculas.Count(m => m.Status == MatriculaStatus.Ativa) < Vagas;
        if (!vagasDisponiveis) return false;
        return true;
    }
}
