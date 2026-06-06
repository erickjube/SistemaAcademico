using SistemaAcademico.DTOs.CursoDto;
using SistemaAcademico.DTOs.PeriodoLetivoDto;

namespace SistemaAcademico.Services.Interfaces;

public interface IPeriodoLetivoService
{
    Task<IEnumerable<PeriodoDto>> ObterTodosPeriodosAsync();
    Task<PeriodoDto?> ObterPeriodoPorIdAsync(int periodoId);
    Task<PeriodoDto> CriarPeriodoAsync(CriarPeriodoDto dto);
    Task ExcluirPeriodoAsync(int periodoId);
}
