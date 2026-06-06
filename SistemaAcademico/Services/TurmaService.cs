using SistemaAcademico.DTOs.TurmaDto;
using SistemaAcademico.Models;
using SistemaAcademico.Repositories.Interfaces;
using SistemaAcademico.Services.Interfaces;

namespace SistemaAcademico.Services;

public class TurmaService : ITurmaService
{
    private readonly ITurmaRepository _turmaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TurmaService(ITurmaRepository turmaRepository, IUnitOfWork unitOfWork)
    {
        _turmaRepository = turmaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TurmaResponseDto>> ObterTodosAsync()
    {
        var turmas = await _turmaRepository.ObterTodosAsync();
        return turmas.Select(t => new TurmaResponseDto
        {
            Id = t.Id,
            DisciplinaId = t.DisciplinaId,
            ProfessorId = t.ProfessorId,
            PeriodoId = t.PeriodoLetivoId,
            Vagas = t.Vagas,
            Fechada = t.Fechada,
            FormulaMediaTipo = t.FormulaMediaTipo,
            DataFechamento = t.DataFechamento
        });
    }

    public async Task<TurmaResponseDto?> ObterPorIdAsync(int turmaId)
    {
        var turma = await _turmaRepository.ObterPorIdAsync(turmaId);
        if (turma == null) throw new Exception("Turma não encontrada");
        return new TurmaResponseDto
        {
            Id = turma.Id,
            DisciplinaId = turma.DisciplinaId,
            ProfessorId = turma.ProfessorId,
            PeriodoId = turma.PeriodoLetivoId,
            Vagas = turma.Vagas,
            Fechada = turma.Fechada,
            FormulaMediaTipo = turma.FormulaMediaTipo,
            DataFechamento = turma.DataFechamento
        };
    }

    public async Task<TurmaResponseDto> CriarAsync(CriarTurmaDto dto)
    {
        var turma = new Turma(
            dto.DisciplinaId,
            dto.ProfessorId,
            dto.PeriodoLetivoId,
            dto.Vagas,
            dto.FormulaMediaTipo
        );
        await _turmaRepository.AdicionarAsync(turma);
        await _unitOfWork.SalvarAsync();

        return new TurmaResponseDto
        {
            DisciplinaId = dto.DisciplinaId,
            ProfessorId = dto.ProfessorId,
            PeriodoId = dto.PeriodoLetivoId,
            Vagas = dto.Vagas,
            FormulaMediaTipo = dto.FormulaMediaTipo,
        };
    }

    public async Task Update(UpdateTurmaDto dto)
    {
        var turma = await _turmaRepository.ObterPorIdAsync(dto.Id);
        if (turma == null) throw new Exception("Turma não encontrada");
        turma.AtualizarVagas(dto.Vagas);
        await _unitOfWork.SalvarAsync();
    }
}
