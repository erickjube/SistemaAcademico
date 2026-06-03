using SistemaAcademico.DTOs.MatriculaDto;
using SistemaAcademico.DTOs.MatriculaEspecialDto;
using SistemaAcademico.Models;

namespace SistemaAcademico.Services.Interfaces;

public interface IMatriculaService
{

    Task<IEnumerable<MatriculaResponseDto>> ObterMatriculasAsync();
    Task<IEnumerable<MatriculaResponseDto?>> ObterMatriculaAsync(int alunoId);
    Task<MatriculaResponseDto> ObterMatriculaPorIdAsync(int matriculaId);
    Task CriarMatriculaAsync(MatriculaCriarDto dto);
    Task CancelarMatriculaAsync(int matriculaId);
}
