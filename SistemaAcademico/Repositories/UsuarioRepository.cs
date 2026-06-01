using Microsoft.EntityFrameworkCore;
using SistemaAcademico.Data;
using SistemaAcademico.ENUMs;
using SistemaAcademico.Models;
using SistemaAcademico.Repositories.Interfaces;

namespace SistemaAcademico.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;
    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> ObterPorEmailAsync(string email)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Usuario?> ObterPorIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task<List<Usuario>> ObterProfessoresAsync()
    {
        return await _context.Usuarios
            .Where(u => u.Perfil == PerfilUsuario.Professor)
            .ToListAsync();
    }

    public Task<List<Usuario>> ObterAlunosAsync()
    {
        return _context.Usuarios
            .Where(u => u.Perfil == PerfilUsuario.Aluno)
            .ToListAsync(); 
    }

    public async Task AdicionarAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
    }

    public async Task<bool> ExisteEmailAsync(string email)
    {
        return await _context.Usuarios.AnyAsync(u => u.Email == email);
    }
}
