using SistemaAcademico.Models;

namespace SistemaAcademico.Repositories.Interfaces;

public interface ISolicitacaoMatriculaEspecialRepository
{
        Task<IEnumerable<SolicitacaoMatriculaEspecial>> ObterSolicitacoesAsync();
        Task<SolicitacaoMatriculaEspecial?> ObterSolicitacaoAsync(int solicitacaoId);
        Task AdicionarSolicitacaoMatriculaEspecialAsync(SolicitacaoMatriculaEspecial solicitacao);
}
