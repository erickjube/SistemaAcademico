using SistemaAcademico.Models;

namespace SistemaAcademico.Repositories.Interfaces;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> ObterTodosAsync();
    Task<Usuario?> ObterPorEmailAsync(string email);
    Task<Usuario?> ObterPorIdAsync(int id);
    Task<IEnumerable<Usuario>> ObterProfessoresAsync();
    Task<IEnumerable<Usuario>> ObterAlunosAsync();
    Task AdicionarAsync(Usuario usuario);
    Task<bool> ExisteEmailAsync(string email);
    Task<bool> ExisteCpfAsync(string cpf);
}
