using Microsoft.EntityFrameworkCore;
using SistemaAcademico.Data;
using SistemaAcademico.ENUMs;
using SistemaAcademico.Models;
using SistemaAcademico.Repositories.Interfaces;

namespace SistemaAcademico.Repositories;

public class MatriculaRepository : IMatriculaRepository
{
    private readonly AppDbContext _context;

    public MatriculaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Matricula>> ObterMatriculasAsync()
    {
        return await _context.Matriculas
            .Include(m => m.Aluno)
            .Include(m => m.Turma)
                .ThenInclude(t => t.Disciplina)
            .ToListAsync();
    }

    public async Task<IEnumerable<Matricula>> ObterMatriculasPorAlunoAsync(int alunoId)
    {
        return await _context.Matriculas
            .Include(m => m.Turma)
                .ThenInclude(t => t.Disciplina)
            .Where(m => m.AlunoId == alunoId)
            .ToListAsync();
    }

    public async Task<Matricula> ObterMatriculaAsync(int matriculaId)
    {
        return await _context.Matriculas
            .Include(m => m.Aluno)
            .Include(m => m.Turma)
                .ThenInclude(t => t.Disciplina)
            .FirstOrDefaultAsync(m => m.Id == matriculaId);
    }

    public Task AdicionarFilaEsperaAsync(FilaEspera fila)
    {
        return _context.FilaEsperas.AddAsync(fila).AsTask();
    }

    public Task AdicionarMatriculaAsync(Matricula matricula)
    {
        return _context.Matriculas.AddAsync(matricula).AsTask();
    }

    public Task<int> ContarMatriculasAtivasAsync(int turmaId)
    {
        return _context.Matriculas.CountAsync(m => m.TurmaId == turmaId && m.Status == ENUMs.MatriculaStatus.Ativa);
    }

    public Task<bool> AlunoJaMatriculadoAsync(int alunoId, int disciplinaId)
    {
        return _context.Matriculas.AnyAsync(m => m.AlunoId == alunoId && 
                                            m.Turma!.DisciplinaId == disciplinaId && 
                                            m.Status == ENUMs.MatriculaStatus.Ativa);
    }

    public Task<int> QuantidadeDisciplinasMatriculadasAsync(int alunoId, int periodoLetivoId)
    {
        return _context.Matriculas.CountAsync(m => m.AlunoId == alunoId && 
                                            m.Turma!.PeriodoLetivoId == periodoLetivoId && 
                                            m.Status == ENUMs.MatriculaStatus.Ativa);
    }
    
    public Task<bool> PossuiPreRequisitoAsync(int alunoId, int disciplinaId)
    {
        return _context.Matriculas.AnyAsync(m =>
                                            m.AlunoId == alunoId &&
                                            m.Nota != null &&
                                            m.Nota!.Situacao == ENUMs.SituacaoAluno.Aprovado &&
                                            m.Turma!.DisciplinaId == disciplinaId);
    }

    public async Task<Turma?> ObterTurmaCompletaAsync(int turmaId)
    {
        return await _context.Turmas
            .Include(t => t.PeriodoLetivo)
            .Include(t => t.Disciplina)!
                .ThenInclude(d => d!.PreRequisito)
            .Include(t => t.Matriculas)
            .FirstOrDefaultAsync(t => t.Id == turmaId);
    }

    public Task<Usuario?> ObterAlunoAsync(int alunoId)
    {
        return _context.Usuarios.FirstOrDefaultAsync(u => u.Id == alunoId && u.Perfil == PerfilUsuario.Aluno);
    }

    public async Task<IEnumerable<Turma>> ObterTurmasDisponiveisAsync()
    {
        return await _context.Turmas
            .Include(t => t.Disciplina)
            .Include(t => t.Professor)
            .Include(t => t.PeriodoLetivo)
            .Include(t => t.Matriculas)
            .ToListAsync();
    }
}
