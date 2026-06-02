using SistemaAcademico.DTOs.MatriculaDto;
using SistemaAcademico.ENUMs;
using SistemaAcademico.Models;
using SistemaAcademico.Repositories.Interfaces;
using SistemaAcademico.Services.Interfaces;

namespace SistemaAcademico.Services;

public class MatriculaService : IMatriculaService
{
    private readonly IMatriculaRepository _matriculaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFilaEsperaRepository _filaEsperaRepository;
    public MatriculaService(IMatriculaRepository matriculaRepository, 
                            IUnitOfWork unitOfWork, 
                            IFilaEsperaRepository filaEsperaRepository)
    {
        _matriculaRepository = matriculaRepository;
        _unitOfWork = unitOfWork;
        _filaEsperaRepository = filaEsperaRepository;
    }

    public async Task<IEnumerable<MatriculaResponseDto>> ObterMatriculasAsync()
    {
        var matriculas = await _matriculaRepository.ObterMatriculasAsync();

        return matriculas.Select(m => new MatriculaResponseDto
        {
            Id = m.Id,
            AlunoId = m.AlunoId,
            TurmaId = m.TurmaId,
            Status = m.Status.ToString(),
            DataMatricula = m.DataMatricula
        });
    }
    
    public async Task<IEnumerable<MatriculaResponseDto?>> ObterMatriculaAsync(int alunoId)
    {
        var matriculas = await _matriculaRepository.ObterMatriculasPorAlunoAsync(alunoId);
        if (matriculas == null) throw new Exception("Matrículas não encontradas para o aluno especificado.");

        return matriculas.Select(m => new MatriculaResponseDto
        {
            Id = m.Id,
            AlunoId = m.AlunoId,
            TurmaId = m.TurmaId,
            Status = m.Status.ToString(),
            DataMatricula = m.DataMatricula
        });
    }

    public async Task<MatriculaResponseDto> ObterMatriculaPorIdAsync(int matriculaId)
    {
        var matricula = await _matriculaRepository.ObterMatriculaAsync(matriculaId);
        if (matricula == null) throw new Exception("Matrícula não encontrada para o ID especificado.");

        return new MatriculaResponseDto
        {
            Id = matricula.Id,
            AlunoId = matricula.AlunoId,
            TurmaId = matricula.TurmaId,
            Status = matricula.Status.ToString(),
            DataMatricula = matricula.DataMatricula
        };
    }

    public async Task CriarMatriculaAsync(MatriculaCriarDto dto)
    {
        var aluno = await _matriculaRepository.ObterAlunoAsync(dto.AlunoId);
        if (aluno == null) throw new Exception("Aluno não encontrado para o ID especificado.");

        var turma = await _matriculaRepository.ObterTurmaCompletaAsync(dto.TurmaId);
        if (turma == null) throw new Exception("Turma não encontrada para o ID especificado.");

        turma.PeriodoLetivo?.ValidarPeriodoMatricula();

        var jaMatriculado = await _matriculaRepository.AlunoJaMatriculadoAsync(dto.AlunoId, dto.TurmaId);
        if (jaMatriculado) throw new Exception("Aluno já está matriculado nesta turma.");

        var qtdeDisciplinas = await _matriculaRepository.QuantidadeDisciplinasMatriculadasAsync(dto.AlunoId, turma.PeriodoLetivoId);
        const int LIMITE_DISCIPLINAS = 6;
        if (qtdeDisciplinas >= LIMITE_DISCIPLINAS) throw new Exception("Limite de disciplinas excedido.");

        if (turma.Disciplina?.PreRequisito != null)
        {
            var possuiPreRequisito =
                await _matriculaRepository.PossuiPreRequisitoAsync(
                    dto.AlunoId,
                    turma.Disciplina.PreRequisito.Id);

            if (!possuiPreRequisito) throw new Exception("Pré-requisito não cumprido.");
        }

        if (!turma.ValidarVagas())
        {
            var filaEspera = new FilaEspera(
                dto.AlunoId,
                dto.TurmaId);

            await _filaEsperaRepository.AdicionarFilaEsperaAsync(filaEspera);
            throw new Exception("Turma sem vagas disponíveis. Aluno adicionado à fila de espera.");
        }
        
        var matricula = new Matricula(
            dto.AlunoId,
            dto.TurmaId);

        await _matriculaRepository.AdicionarMatriculaAsync(matricula);
        await _unitOfWork.SalvarAsync();

    }
}
