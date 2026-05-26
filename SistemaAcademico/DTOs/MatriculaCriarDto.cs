namespace SistemaAcademico.DTOs;

public class MatriculaCriarDto
{
    public int AlunoId { get; set; }
    public int TurmaId { get; set; }
    public string? Justificativa { get; set; }
}