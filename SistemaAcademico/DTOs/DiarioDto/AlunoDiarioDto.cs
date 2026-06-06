using SistemaAcademico.ENUMs;

namespace SistemaAcademico.DTOs.DiarioDto;

public class AlunoDiarioDto
{
    public int MatriculaId { get; set; }
    public string NomeAluno { get; set; }

    public decimal? P1 { get; set; }
    public decimal? P2 { get; set; }
    public decimal? Trabalho { get; set; }

    public decimal? Frequencia { get; set; }

    public decimal? Media { get; set; }

    public SituacaoAluno Situacao { get; set; }
}
