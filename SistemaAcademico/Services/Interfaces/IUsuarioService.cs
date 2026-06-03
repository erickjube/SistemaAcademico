using SistemaAcademico.DTOs.UsuarioDto;
using SistemaAcademico.Models;

namespace SistemaAcademico.Services.Interfaces;

public interface IUsuarioService
{
    Task<UsuarioResponseDto?> ObterPorEmailAsync(string email);
    Task<UsuarioResponseDto?> ObterPorIdAsync(int UsuarioId);
    Task<IEnumerable<UsuarioResponseDto>> ObterProfessoresAsync();
    Task<IEnumerable<UsuarioResponseDto>> ObterAlunosAsync();
    Task AdicionarAsync(CriarUsuarioDto dto);
    Task AtualizarAsync(AtualizarUsuarioDto dto);
}
