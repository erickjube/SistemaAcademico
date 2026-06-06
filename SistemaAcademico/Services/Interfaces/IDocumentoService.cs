using SistemaAcademico.Models;

namespace SistemaAcademico.Services.Interfaces;

public interface IDocumentoService
{
    Task<byte[]> GerarHistoricoAsync(int usuarioId);
    Task<byte[]> GerarDeclaracaoAsync(int usuarioId);
    Task<bool> ValidarDocumentoAsync(string hash);
}
