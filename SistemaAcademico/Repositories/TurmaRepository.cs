using Microsoft.EntityFrameworkCore;
using SistemaAcademico.Data;
using SistemaAcademico.Models;
using SistemaAcademico.Repositories.Interfaces;

namespace SistemaAcademico.Repositories;

public class TurmaRepository : ITurmaRepository
{
    private readonly AppDbContext _context;

    public TurmaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Turma>> ObterTodosAsync()
    {
        return await _context.Turmas.ToListAsync();
    }

    public async Task<Turma?> ObterPorIdAsync(int turmaId)
    {
        return await _context.Turmas.FirstOrDefaultAsync(t => t.Id == turmaId);
    }

    public async Task AdicionarAsync(Turma turma)
    {
        await _context.Turmas.AddAsync(turma);
    }
}
