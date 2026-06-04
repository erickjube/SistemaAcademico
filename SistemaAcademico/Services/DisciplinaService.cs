using SistemaAcademico.DTOs.DisciplinaDto;
using SistemaAcademico.Models;
using SistemaAcademico.Repositories.Interfaces;
using SistemaAcademico.Services.Interfaces;

namespace SistemaAcademico.Services;

public class DisciplinaService : IDisciplinaService
{
    private readonly IDisciplinaRepository _disciplinaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DisciplinaService(IDisciplinaRepository disciplinaRepository, IUnitOfWork unitOfWork)
    {
        _disciplinaRepository = disciplinaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<DisciplinaDto>> ObterTodasDisciplinasAsync()
    {
        var disciplinas = await _disciplinaRepository.GetAllAsync();
        return disciplinas.Select(d => new DisciplinaDto
        {
            Id = d.Id,
            Nome = d.Nome,
            CursoId = d.CursoId,
            CargaHoraria = d.CargaHoraria,
            PreRequisitoId = d.PreRequisitoId
        });
    }

    public async Task<DisciplinaDto> ObterDisciplinaPorIdAsync(int disciplinaId)
    {
        var disciplina = await _disciplinaRepository.GetByIdAsync(disciplinaId);
        if (disciplina == null) throw new KeyNotFoundException("Disciplina não encontrada.");
        return new DisciplinaDto
        {
            Id = disciplina.Id,
            Nome = disciplina.Nome,
            CursoId = disciplina.CursoId,
            CargaHoraria = disciplina.CargaHoraria,
            PreRequisitoId = disciplina.PreRequisitoId
        };
    }

    public async Task<DisciplinaDto> CriarDisciplinaAsync(DisciplinaDto dto)
    {

        var disciplina = new Disciplina(dto.Nome, dto.CursoId, dto.CargaHoraria, dto.PreRequisitoId);
        await _unitOfWork.SalvarAsync();
        await _disciplinaRepository.AddAsync(disciplina);
        return new DisciplinaDto
        {
            Id = disciplina.Id,
            Nome = disciplina.Nome,
            CursoId = disciplina.CursoId,
            CargaHoraria = disciplina.CargaHoraria,
            PreRequisitoId = disciplina.PreRequisitoId
        };
    }

    public async Task AtualizarDisciplinaAsync(DisciplinaDto dto)
    {
        var disciplina = await _disciplinaRepository.GetByIdAsync(dto.Id);
        if (disciplina == null) throw new Exception("Disciplina não encontrada.");
        disciplina.Atualizar(dto.Nome, dto.CargaHoraria, dto.PreRequisitoId);
        await _unitOfWork.SalvarAsync();
    }

    public async Task ExcluirDisciplinaAsync(int disciplinaId)
    {
        var disciplina = await _disciplinaRepository.GetByIdAsync(disciplinaId);
        if (disciplina == null) throw new Exception("Disciplina não encontrada.");
        await _disciplinaRepository.DeleteAsync(disciplina);
        await _unitOfWork.SalvarAsync();
    }
}