using SistemaAcademico.Models;

namespace SistemaAcademico.Repositories.Interfaces;

public interface IDisciplinaRepository
{
    Task<IEnumerable<Disciplina>> GetAllAsync();
    Task<Disciplina> GetByIdAsync(int disciplinaId);
    Task AddAsync(Disciplina disciplina);
    Task DeleteAsync(Disciplina disciplina);
}
