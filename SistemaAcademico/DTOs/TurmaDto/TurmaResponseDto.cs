using SistemaAcademico.ENUMs;

namespace SistemaAcademico.DTOs.TurmaDto;

public class TurmaResponseDto
{
    public int Id { get; set; }
    public int DisciplinaId { get; set; }   
    public int ProfessorId { get; set; }
    public int PeriodoId { get; set; } 
    public int Vagas { get; set; }
    public bool Fechada { get; set; }
    public FormulaMediaTipo FormulaMediaTipo { get; set; }
    public DateTime? DataFechamento { get; set; }
}
