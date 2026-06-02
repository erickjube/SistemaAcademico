using SistemaAcademico.ENUMs;

namespace SistemaAcademico.DTOs.NotaDto;

public class NotaResponseDto
{
    public int MatriculaId { get; set; }
    public string NomeAluno { get; set; } = string.Empty;
    public decimal P1 { get; set; }
    public decimal P2 { get; set; }
    public decimal Trabalho { get; set; }
    public decimal Media { get; set; }
    public decimal Frequencia { get; set; }
    public SituacaoAluno Situacao { get; set; } 
}
