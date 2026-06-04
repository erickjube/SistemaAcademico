using SistemaAcademico.DTOs.CursoDto;
using SistemaAcademico.Repositories.Interfaces;
using SistemaAcademico.Services.Interfaces;
using SistemaAcademico.Models;

namespace SistemaAcademico.Services;

public class CursoService : ICursoService
{
    private readonly ICursoRepository _cursoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CursoService(ICursoRepository cursoRepository, IUnitOfWork unitOfWork)
    {
        _cursoRepository = cursoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CursoDto>> ObterTodosCursosAsync()
    {
        var cursos = await _cursoRepository.ObterTodosAsync();
        return cursos.Select(c => new CursoDto
        {
            Id = c.Id,
            Nome = c.Nome,
            MaxDisciplinas = c.MaxDisciplinas
        });
    }

    public async Task<CursoDto?> ObterCursoPorIdAsync(int cursoId)
    {
        var curso = await _cursoRepository.ObterPorIdAsync(cursoId);
        if (curso == null) throw new Exception("Curso não encontrado");
        return new CursoDto
        {
            Id = curso.Id,
            Nome = curso.Nome,
            MaxDisciplinas = curso.MaxDisciplinas
        };
    }

    public async Task<CursoDto> CriarCursoAsync(CriarCursoDto dto)
    {
        var curso = new Curso(dto.Nome, dto.MaxDisciplinas);
        await _cursoRepository.AdicionarAsync(curso);
        await _unitOfWork.SalvarAsync();
        return new CursoDto
        {
            Id = curso.Id,
            Nome = curso.Nome,
            MaxDisciplinas = curso.MaxDisciplinas
        };
    }

    public async Task<CursoDto> AtualizarCursoAsync(CursoDto dto)
    {
        var curso = await _cursoRepository.ObterPorIdAsync(dto.Id);
        if (curso == null) throw new Exception("Curso não encontrado");
        curso.Atualizar(dto.Nome, dto.MaxDisciplinas);
        await _unitOfWork.SalvarAsync();
        return new CursoDto
        {
            Id = curso.Id,
            Nome = curso.Nome,
            MaxDisciplinas = curso.MaxDisciplinas
        };
    }   

    public async Task ExcluirCursoAsync(int cursoId)
    {
        var curso = await _cursoRepository.ObterPorIdAsync(cursoId);
        if (curso == null) throw new Exception("Curso não encontrado");
        _cursoRepository.Remover(curso);
        await _unitOfWork.SalvarAsync();
    }
}
