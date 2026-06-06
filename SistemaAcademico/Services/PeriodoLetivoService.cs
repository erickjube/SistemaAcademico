using SistemaAcademico.DTOs.PeriodoLetivoDto;
using SistemaAcademico.Models;
using SistemaAcademico.Repositories;
using SistemaAcademico.Repositories.Interfaces;
using SistemaAcademico.Services.Interfaces;

namespace SistemaAcademico.Services;

public class PeriodoLetivoService : IPeriodoLetivoService
{
    private readonly IPeriodoLetivoRepository _periodoLetivoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PeriodoLetivoService(IPeriodoLetivoRepository periodoLetivoRepository, IUnitOfWork unitOfWork)
    {
        _periodoLetivoRepository = periodoLetivoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PeriodoDto>> ObterTodosPeriodosAsync()
    {
        var periodos = await _periodoLetivoRepository.ObterTodosAsync();
        return periodos.Select(p => new PeriodoDto
        {
            Id = p.Id,
            Semestre = p.Semestre,
            InicioMatricula = p.InicioMatricula,
            FimMatricula = p.FimMatricula,
            InicioAulas = p.InicioAulas,
            FimAulas = p.FimAulas,
        });
    }

    public async Task<PeriodoDto?> ObterPeriodoPorIdAsync(int periodoId)
    {
        var periodo = await _periodoLetivoRepository.ObterPorIdAsync(periodoId);
        if (periodo == null) throw new Exception("Periodo Letivo não encontrado");
        return new PeriodoDto
        {
            Id = periodo.Id,
            Semestre = periodo.Semestre,
            InicioMatricula = periodo.InicioMatricula,
            FimMatricula = periodo.FimMatricula,
            InicioAulas = periodo.InicioAulas,
            FimAulas = periodo.FimAulas,
        };
    }

    public async Task<PeriodoDto> CriarPeriodoAsync(CriarPeriodoDto dto)
    {
        var periodo = new PeriodoLetivo(dto.Semestre, dto.InicioMatricula, dto.FimMatricula, dto.InicioAulas, dto.FimAulas);
        await _periodoLetivoRepository.AdicionarAsync(periodo);
        await _unitOfWork.SalvarAsync();
        return new PeriodoDto
        {
            Id = periodo.Id,
            Semestre = periodo.Semestre,
            InicioMatricula = periodo.InicioMatricula,
            FimMatricula = periodo.FimMatricula,
            InicioAulas = periodo.InicioAulas,
            FimAulas = periodo.FimAulas,
        };
    }

    public async Task ExcluirPeriodoAsync(int periodoId)
    {
        var periodo = await _periodoLetivoRepository.ObterPorIdAsync(periodoId);
        if (periodo == null) throw new Exception("Periodo não encontrado");
        _periodoLetivoRepository.Remover(periodo);
        await _unitOfWork.SalvarAsync();
    }
}
