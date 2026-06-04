using SistemaAcademico.Models;

namespace SistemaAcademico.Repositories.Interfaces;

public interface ICursoRepository
{
    Task <IEnumerable<Curso>> ObterTodosAsync();
    Task<Curso> ObterPorIdAsync(int id);
    Task AdicionarAsync(Curso curso);
    void Remover(Curso curso);
}
