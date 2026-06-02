using SistemaAcademico.DTOs.MatriculaDto;
using SistemaAcademico.Models;

namespace SistemaAcademico.Services.Interfaces;

public interface IMatriculaService
{

    Task<IEnumerable<MatriculaResponseDto>> ObterMatriculasAsync();
    Task<IEnumerable<MatriculaResponseDto?>> ObterMatriculaAsync(int alunoId);
    Task<MatriculaResponseDto> ObterMatriculaPorIdAsync(int matriculaId);
    Task CriarMatriculaAsync(MatriculaCriarDto dto);
    Task CancelarMatriculaAsync(int id);
    Task ColocarEmFilaEsperaAsync(int id);
    Task AprovarMatriculaEspecialAsync(int id);
    Task RejeitarMatriculaEspecialAsync(int id);
}
