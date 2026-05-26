namespace SistemaAcademico.DTOs;

public class NotaAtualizarDto
{
    public int MatriculaId { get; set; }
    public decimal P1 { get; set; }
    public decimal P2 { get; set; }
    public decimal Trabalho { get; set; }
    public decimal Frequencia { get; set; }
    public string? Motivo { get; set; }
}