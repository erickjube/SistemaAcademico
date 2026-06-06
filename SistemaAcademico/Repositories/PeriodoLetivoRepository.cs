using Microsoft.EntityFrameworkCore;
using SistemaAcademico.Data;
using SistemaAcademico.Models;
using SistemaAcademico.Repositories.Interfaces;

namespace SistemaAcademico.Repositories;

public class PeriodoLetivoRepository : IPeriodoLetivoRepository
{
    private readonly AppDbContext _context;

    public PeriodoLetivoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PeriodoLetivo>> ObterTodosAsync()
    {
        return await _context.PeriodosLetivos.ToListAsync();
    }

    public async Task<PeriodoLetivo?> ObterPorIdAsync(int periodoId)
    {
        return await _context.PeriodosLetivos.FindAsync(periodoId);
    }

    public async Task AdicionarAsync(PeriodoLetivo periodo)
    {
        await _context.PeriodosLetivos.AddAsync(periodo);
    }

    public void Remover(PeriodoLetivo periodo)
    {
        _context.PeriodosLetivos.Remove(periodo);
    }

}
