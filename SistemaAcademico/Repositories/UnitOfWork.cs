using SistemaAcademico.Data;
using SistemaAcademico.Repositories.Interfaces;

namespace SistemaAcademico.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task SalvarAsync()
    {
        await _context.SaveChangesAsync();
    }
}
