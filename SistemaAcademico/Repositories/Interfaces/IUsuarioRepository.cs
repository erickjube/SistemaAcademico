using SistemaAcademico.Models;

namespace SistemaAcademico.Repositories.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorEmailAsync(string email);
    Task<Usuario?> ObterPorIdAsync(int id);
    Task<List<Usuario>> ObterProfessoresAsync();
    Task<List<Usuario>> ObterAlunosAsync();
    Task AdicionarAsync(Usuario usuario);
    Task<bool> ExisteEmailAsync(string email);
}
