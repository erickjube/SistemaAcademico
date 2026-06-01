using SistemaAcademico.Models;

namespace SistemaAcademico.Repositories.Interfaces;

public interface INotaRepository
{
    Task<Matricula?> ObterMatriculaAsync(int matriculaId);
    Task<Turma?> ObterTurmaAsync(int turmaId);
    Task<Nota?> ObterNotaAsync(int matriculaId);
    Task AdicionarNotaAsync(Nota nota);
    void AtualizarNota(Nota nota);
}
