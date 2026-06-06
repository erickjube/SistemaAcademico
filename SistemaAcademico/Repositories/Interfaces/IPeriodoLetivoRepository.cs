using SistemaAcademico.Models;

namespace SistemaAcademico.Repositories.Interfaces;

public interface IPeriodoLetivoRepository
{
    Task<IEnumerable<PeriodoLetivo>> ObterTodosAsync();
    Task<PeriodoLetivo> ObterPorIdAsync(int periodoId);
    Task AdicionarAsync(PeriodoLetivo periodo);
    void Remover(PeriodoLetivo periodo);
}
