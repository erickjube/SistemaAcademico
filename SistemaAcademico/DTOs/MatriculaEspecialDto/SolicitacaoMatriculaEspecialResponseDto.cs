using SistemaAcademico.ENUMs;

namespace SistemaAcademico.DTOs.MatriculaEspecialDto;

public class SolicitacaoMatriculaEspecialResponseDto
{
    public int Id { get; set; }
    public int AlunoId { get; set; }
    public string AlunoNome { get; set; } = null!;
    public int TurmaId { get; set; }
    public string TurmaNome { get; set; } = null!;
    public string Justificativa { get; set; } = null!;
    public DateTime DataSolicitacao { get; set; }
    public SolicitacaoStatus Status { get; set; }
}
