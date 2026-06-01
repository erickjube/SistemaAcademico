using Microsoft.EntityFrameworkCore;
using SistemaAcademico.Data;
using SistemaAcademico.Models;
using SistemaAcademico.Repositories.Interfaces;

namespace SistemaAcademico.Repositories;

public class NotaRepository : INotaRepository
{
    private readonly AppDbContext _context;

    public NotaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Matricula?> ObterMatriculaAsync(int matriculaId)
    {
        return await _context.Matriculas
            .Include(m => m.Aluno)
            .Include(m => m.Turma)
            .FirstOrDefaultAsync(m => m.Id == matriculaId);
    }

    public async Task<Turma?> ObterTurmaAsync(int turmaId)
    {
        return await _context.Turmas
            .Include(t => t.Disciplina)
            .FirstOrDefaultAsync(t => t.Id == turmaId);
    }

    public async Task<Nota?> ObterNotaAsync(int matriculaId)
    {
        return await _context.Notas.FirstOrDefaultAsync(n => n.MatriculaId == matriculaId);
    }

    public async Task AdicionarNotaAsync(Nota nota)
    {
        await _context.Notas.AddAsync(nota);
    }

    public void AtualizarNota(Nota nota)
    {
        _context.Notas.Update(nota);
    }
}
