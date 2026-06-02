using SistemaAcademico.DTOs.MatriculaDto;
using SistemaAcademico.Models;

namespace SistemaAcademico.Services.Interfaces;

public interface IMatriculaService
{
    Task<IEnumerable<MatriculaResponseDto>> ObterMatriculasAsync();
    Task<MatriculaResponseDto?> ObterMatriculaAsync(int id);
    Task<MatriculaResultadoDto> CriarMatriculaAsync(MatriculaCriarDto dto);
    Task CancelarMatriculaAsync(int id);
}
