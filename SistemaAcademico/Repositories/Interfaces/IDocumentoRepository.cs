using SistemaAcademico.Models;

namespace SistemaAcademico.Repositories.Interfaces;

public interface IDocumentoRepository
{
    Task AddAsync(DocumentoGerado documento);
    Task<DocumentoGerado?> ObterPorHashAsync(string hash);
    Task<IEnumerable<DocumentoGerado>> ObterPorUsuarioAsync(int usuarioId);
}
