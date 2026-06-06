using SistemaAcademico.Models;

namespace SistemaAcademico.Repositories.Interfaces;

public interface ICursoRepository
{
    Task <IEnumerable<Curso>> ObterTodosAsync();
    Task<Curso> ObterPorIdAsync(int cursoId);
    Task AdicionarAsync(Curso curso);
    void Remover(Curso curso);
}
