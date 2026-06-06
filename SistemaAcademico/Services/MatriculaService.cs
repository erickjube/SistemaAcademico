using SistemaAcademico.DTOs.MatriculaDto;
using SistemaAcademico.DTOs.MatriculaEspecialDto;
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
    private readonly ISolicitacaoMatriculaEspecialRepository _solicitacaoMatriculaEspecialRepository;

    public MatriculaService(IMatriculaRepository matriculaRepository, 
                            IUnitOfWork unitOfWork, 
                            IFilaEsperaRepository filaEsperaRepository,
                            ISolicitacaoMatriculaEspecialRepository solicitacaoMatriculaEspecialRepository)
    {
        _matriculaRepository = matriculaRepository;
        _unitOfWork = unitOfWork;
        _filaEsperaRepository = filaEsperaRepository;
        _solicitacaoMatriculaEspecialRepository = solicitacaoMatriculaEspecialRepository;
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

    public async Task<IEnumerable<TurmaDisponivelDto>> ObterTurmasDisponiveisAsync(int alunoId)
    {
        var aluno = await _matriculaRepository.ObterAlunoAsync(alunoId);
        if (aluno == null) throw new Exception("Aluno não encontrado.");

        var turmas = await _matriculaRepository.ObterTurmasDisponiveisAsync();

        var resultado = new List<TurmaDisponivelDto>();

        foreach (var turma in turmas)
        {
            try
            {
                turma.PeriodoLetivo?.ValidarPeriodoMatricula();
            }
            catch
            {
                continue;
            }

            var jaMatriculado = await _matriculaRepository.AlunoJaMatriculadoAsync(alunoId, turma.Id);
            if (jaMatriculado) continue;

            var possuiPreRequisito = true;

            if (turma.Disciplina?.PreRequisito != null)
                possuiPreRequisito = await _matriculaRepository.PossuiPreRequisitoAsync(alunoId, turma.Disciplina.PreRequisito.Id);

            resultado.Add(new TurmaDisponivelDto
            {
                TurmaId = turma.Id,
                Disciplina = turma.Disciplina?.Nome ?? "",
                Professor = turma.Professor?.Nome ?? "",
                Vagas = turma.Vagas,
                VagasOcupadas = turma.Matriculas.Count(m => m.Status == MatriculaStatus.Ativa),
                PossuiVagas = turma.ValidarVagas(),
                PossuiPreRequisito = possuiPreRequisito,
                MatriculaPermitida = possuiPreRequisito
            });
        }
        return resultado;
    }

    public async Task<MatriculaResponseDto> CriarMatriculaAsync(MatriculaCriarDto dto)
    {
        var aluno = await _matriculaRepository.ObterAlunoAsync(dto.AlunoId);
        if (aluno == null) throw new Exception("Aluno não encontrado para o ID especificado.");

        if (!aluno.Ativo) throw new InvalidOperationException("Usuário está desativado.");

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
        return new MatriculaResponseDto
        {
            Id = matricula.Id,
            AlunoId = matricula.AlunoId,
            TurmaId = matricula.TurmaId,
            Status = matricula.Status.ToString(),
            DataMatricula = matricula.DataMatricula
        };
    }

    public async Task CancelarMatriculaAsync(int matriculaId)
    {
        var matricula = await _matriculaRepository.ObterMatriculaAsync(matriculaId);
        if (matricula == null) throw new Exception("Matrícula não encontrada para o ID especificado.");

        var turma = await _matriculaRepository.ObterTurmaCompletaAsync(matricula.TurmaId);
        var dataLimite = turma?.PeriodoLetivo?.InicioAulas.AddDays(-7);
        if (dataLimite == null)
            throw new InvalidOperationException("Não é possível determinar a data limite para cancelamento.");

        if (DateTime.UtcNow > dataLimite)
            throw new Exception("O prazo para cancelamento da matrícula expirou.");

        matricula.Cancelar();
        await _unitOfWork.SalvarAsync();
    }
}
