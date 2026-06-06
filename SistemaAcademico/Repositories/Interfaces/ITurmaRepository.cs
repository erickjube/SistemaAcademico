using SistemaAcademico.Models;

namespace SistemaAcademico.Repositories.Interfaces;

public interface ITurmaRepository
{
    Task<IEnumerable<Turma>> ObterTodosAsync();
    Task<Turma?> ObterPorIdAsync(int turmaId);
    Task AdicionarAsync(Turma turma);
}
