namespace SistemaAcademico.DTOs.MatriculaEspecialDto;

public class SolicitacaoMatriculaEspecialCriarDto
{
    public int AlunoId { get; set; }
    public int TurmaId { get; set; }
    public string Justificativa { get; set; } = null!;
}
