using Microsoft.EntityFrameworkCore;
using SistemaAcademico.Data;
using SistemaAcademico.Models;
using SistemaAcademico.Repositories.Interfaces;

namespace SistemaAcademico.Repositories;

public class DocumentoRepository : IDocumentoRepository
{
    private readonly AppDbContext _context;

    public DocumentoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(DocumentoGerado documento)
    {
        await _context.DocumentosGerados.AddAsync(documento);
    }

    public async Task<DocumentoGerado?> ObterPorHashAsync(string hash)
    {
        return await _context.DocumentosGerados.FirstOrDefaultAsync(x => x.HashSha256 == hash);
    }

    public async Task<IEnumerable<DocumentoGerado>> ObterPorUsuarioAsync(int usuarioId)
    {
        return await _context.DocumentosGerados.Where(u => u.UsuarioId == usuarioId).ToListAsync();
    }
}
