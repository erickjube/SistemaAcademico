using SistemaAcademico.DTOs.DisciplinaDto;

namespace SistemaAcademico.Services.Interfaces;

public interface IDisciplinaService
{
    Task<IEnumerable<DisciplinaResponseDto>> ObterTodasDisciplinasAsync();
    Task<DisciplinaResponseDto> ObterDisciplinaPorIdAsync(int disciplinaId);
    Task<DisciplinaResponseDto> CriarDisciplinaAsync(CriarDisciplinaDto dto);
    Task AtualizarDisciplinaAsync(UpdateDisciplinaDto dto);
    Task ExcluirDisciplinaAsync(int disciplinaId);
}
