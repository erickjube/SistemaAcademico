using SistemaAcademico.DTOs.TurmaDto;

namespace SistemaAcademico.Services.Interfaces;

public interface ITurmaService
{
    Task<IEnumerable<TurmaResponseDto>> ObterTodosAsync();
    Task<TurmaResponseDto?> ObterPorIdAsync(int turmaId);
    Task<TurmaResponseDto> CriarAsync(CriarTurmaDto dto);
    Task Update(UpdateTurmaDto dto);
}
