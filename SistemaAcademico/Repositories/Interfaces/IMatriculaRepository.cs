using SistemaAcademico.Models;

namespace SistemaAcademico.Repositories.Interfaces;

public interface IMatriculaRepository
{
    Task<IEnumerable<Matricula>> ObterMatriculasAsync();
    Task<IEnumerable<Matricula>> ObterMatriculasPorAlunoAsync(int alunoId);
    Task<Matricula> ObterMatriculaAsync(int matriculaId);
    Task<Usuario?> ObterAlunoAsync(int alunoId);
    Task<Turma?> ObterTurmaCompletaAsync(int turmaId);
    Task<bool> AlunoJaMatriculadoAsync(int alunoId, int disciplinaId);
    Task<int> QuantidadeDisciplinasMatriculadasAsync(int alunoId, int periodoLetivoId);
    Task<bool> PossuiPreRequisitoAsync(int alunoId, int disciplinaId);
    Task<int> ContarMatriculasAtivasAsync(int turmaId);
    Task AdicionarMatriculaAsync(Matricula matricula);
    Task AdicionarFilaEsperaAsync(FilaEspera fila);
    Task<IEnumerable<Turma>> ObterTurmasDisponiveisAsync();
}