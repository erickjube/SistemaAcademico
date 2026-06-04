using SistemaAcademico.DTOs.DisciplinaDto;

namespace SistemaAcademico.Services.Interfaces;

public interface IDisciplinaService
{
    Task<IEnumerable<DisciplinaDto>> ObterTodasDisciplinasAsync();
    Task<DisciplinaDto> ObterDisciplinaPorIdAsync(int disciplinaId);
    Task<DisciplinaDto> CriarDisciplinaAsync(DisciplinaDto dto);
    Task AtualizarDisciplinaAsync(DisciplinaDto dto);
    Task ExcluirDisciplinaAsync(int disciplinaId);
}
