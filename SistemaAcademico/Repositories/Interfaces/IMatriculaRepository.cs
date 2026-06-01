using SistemaAcademico.Models;

namespace SistemaAcademico.Repositories.Interfaces;

public interface IMatriculaRepository
{
    Task<Usuario?> ObterAlunoAsync(int alunoId);
    Task<Turma?> ObterTurmaCompletaAsync(int turmaId);
    Task<bool> AlunoJaMatriculadoAsync(int alunoId, int disciplinaId);
    Task<int> ContarMatriculasAtivasAsync(int turmaId);
    Task AdicionarMatriculaAsync(Matricula matricula);
    Task AdicionarFilaEsperaAsync(FilaEspera fila);
}