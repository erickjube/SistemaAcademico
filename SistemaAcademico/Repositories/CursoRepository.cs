using Microsoft.EntityFrameworkCore;
using SistemaAcademico.Data;
using SistemaAcademico.Models;
using SistemaAcademico.Repositories.Interfaces;

namespace SistemaAcademico.Repositories;
public class CursoRepository : ICursoRepository
{
    private readonly AppDbContext _context;

    public CursoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Curso>> ObterTodosAsync()
    {
        return await _context.Cursos.ToListAsync();
    }

    public async Task<Curso?> ObterPorIdAsync(int cursoId)
    {
        return await _context.Cursos.FindAsync(cursoId);
    }

    public async Task AdicionarAsync(Curso curso)
    {
        await _context.Cursos.AddAsync(curso);
    }

    public void Remover(Curso curso)
    {
        _context.Cursos.Remove(curso);
    }
}
