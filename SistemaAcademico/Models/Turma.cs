using SistemaAcademico.ENUMs;

namespace SistemaAcademico.Models;

public class Turma
{
    public int Id { get; set; }

    public int DisciplinaId { get; set; }
    public Disciplina? Disciplina { get; set; }

    public int ProfessorId { get; set; }
    public Usuario? Professor { get; set; }

    public int PeriodoLetivoId { get; set; }
    public PeriodoLetivo? PeriodoLetivo { get; set; }

    public int Vagas { get; set; }
    public bool Fechada { get; set; }

    public FormulaMediaTipo FormulaMediaTipo { get; set; } = FormulaMediaTipo.Ponderada;
    public DateTime? DataFechamento { get; set; }

    public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();

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
    }
}
