using Microsoft.EntityFrameworkCore;
using SistemaAcademico.Data;
using SistemaAcademico.Models;
using SistemaAcademico.Repositories.Interfaces;

namespace SistemaAcademico.Repositories;

public class SolicitacaoMatriculaEspecialRepository : ISolicitacaoMatriculaEspecialRepository
{
    private readonly AppDbContext _context;

    public SolicitacaoMatriculaEspecialRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SolicitacaoMatriculaEspecial>> ObterSolicitacoesAsync()
    {
        return await _context.SolicitacoesMatriculaEspecial
            .Include(s => s.Aluno)
            .Include(s => s.Turma)
            .ToListAsync();
    }

    public async Task<SolicitacaoMatriculaEspecial?> ObterSolicitacaoAsync(int solicitacaoId)
    {
        return await _context.SolicitacoesMatriculaEspecial
            .Include(s => s.Aluno)
            .Include(s => s.Turma)
            .FirstOrDefaultAsync(s => s.Id == solicitacaoId);
    }

    public async Task AdicionarSolicitacaoMatriculaEspecialAsync(SolicitacaoMatriculaEspecial solicitacao)
    {
        await _context.SolicitacoesMatriculaEspecial.AddAsync(solicitacao);
        await _context.SaveChangesAsync();
    }
}
