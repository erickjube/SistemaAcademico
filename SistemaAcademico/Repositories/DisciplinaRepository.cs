using Microsoft.EntityFrameworkCore;
using SistemaAcademico.Data;
using SistemaAcademico.Models;
using SistemaAcademico.Repositories.Interfaces;

namespace SistemaAcademico.Repositories;

public class DisciplinaRepository : IDisciplinaRepository
{
    private readonly AppDbContext _context;

    public DisciplinaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Disciplina>> GetAllAsync()
    {
        return await _context.Disciplinas.ToListAsync();
    }

    public async Task<Disciplina?> GetByIdAsync(int disciplinaId)
    {
        return await _context.Disciplinas.FindAsync(disciplinaId);
    }

    public async Task AddAsync(Disciplina disciplina)
    {
        await _context.Disciplinas.AddAsync(disciplina);
    }

    public async Task DeleteAsync(Disciplina disciplina)
    {
        _context.Disciplinas.Remove(disciplina);
    }
}
