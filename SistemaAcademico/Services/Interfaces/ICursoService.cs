using SistemaAcademico.DTOs.CursoDto;

namespace SistemaAcademico.Services.Interfaces;

public interface ICursoService
{
    Task<IEnumerable<CursoDto>> ObterTodosCursosAsync();
    Task<CursoDto?> ObterCursoPorIdAsync(int cursoId);
    Task<CursoDto> CriarCursoAsync(CriarCursoDto dto);
    Task<CursoDto> AtualizarCursoAsync(CursoDto dto);
    Task ExcluirCursoAsync(int cursoId);
}
