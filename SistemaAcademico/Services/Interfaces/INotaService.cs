using SistemaAcademico.DTOs.DiarioDto;
using SistemaAcademico.DTOs.NotaDto;

namespace SistemaAcademico.Services.Interfaces;

public interface INotaService
{
    Task<IEnumerable<NotaResponseDto>> ObterNotasDaTurmaAsync(int turmaId);
    Task<NotaResponseDto?> ObterNotaAsync(int matriculaId);
    Task LancarNotaAsync(CriarNotaDto dto);
    Task FecharDiarioAsync(int turmaId);
    Task AtualizarNotaAsync(CriarNotaDto dto);
    Task<DiarioDto> BuscarDiarioAsync(int turmaId);
}
