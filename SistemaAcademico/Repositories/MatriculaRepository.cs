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
}
