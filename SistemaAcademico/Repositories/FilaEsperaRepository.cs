using SistemaAcademico.Data;
using SistemaAcademico.Models;
using SistemaAcademico.Repositories.Interfaces;

namespace SistemaAcademico.Repositories;

public class FilaEsperaRepository : IFilaEsperaRepository
{
    private readonly AppDbContext _context;

    public FilaEsperaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarFilaEsperaAsync(FilaEspera fila)
    {
        await _context.FilaEsperas.AddAsync(fila);
    }
}
