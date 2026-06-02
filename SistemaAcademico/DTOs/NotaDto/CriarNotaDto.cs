using SistemaAcademico.ENUMs;

namespace SistemaAcademico.DTOs.NotaDto;

public class CriarNotaDto
{
    public int MatriculaId { get; set; }
    public decimal P1 { get; set; }
    public decimal P2 { get; set; }
    public decimal Trabalho { get; set; }
    public decimal Frequencia { get; set; }
    public FormulaMediaTipo Formula { get; set; }
}