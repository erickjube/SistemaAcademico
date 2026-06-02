namespace SistemaAcademico.DTOs.MatriculaDto;

public class MatriculaResponseDto
{
    public int Id { get; set; }
    public int AlunoId { get; set; }
    public string AlunoNome { get; set; } = string.Empty;
    public int TurmaId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime DataMatricula { get; set; }
    public string? Justificativa { get; set; }
    public decimal? NotaValor { get; set; }
}
