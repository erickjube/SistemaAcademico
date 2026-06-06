using SistemaAcademico.ENUMs;

namespace SistemaAcademico.DTOs.TurmaDto;

public class CriarTurmaDto
{
    public int DisciplinaId { get; set; }
    public int ProfessorId { get; set; }
    public int PeriodoLetivoId { get; set; }
    public int Vagas { get; set; }
    public FormulaMediaTipo FormulaMediaTipo { get; set; }
}
