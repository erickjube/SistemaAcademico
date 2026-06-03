using SistemaAcademico.DTOs.MatriculaEspecialDto;

namespace SistemaAcademico.Services.Interfaces;

public interface ISolicitacaoMatriculaEspecialService
{
    Task<IEnumerable<SolicitacaoMatriculaEspecialResponseDto>> ObterSolicitacoesMatriculaEspecialAsync();
    Task SolicitarMatriculaEspecialAsync(SolicitacaoMatriculaEspecialCriarDto dto);
    Task AprovarMatriculaEspecialAsync(int solicitacaoId);
    Task RejeitarMatriculaEspecialAsync(int solicitacaoId);
}
