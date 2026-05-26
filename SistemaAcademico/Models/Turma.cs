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
}
