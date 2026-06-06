namespace SistemaAcademico.DTOs.MatriculaDto;

public class TurmaDisponivelDto
{
    public int TurmaId { get; set; }
    public string Disciplina { get; set; } = string.Empty;
    public string Professor { get; set; } = string.Empty;
    public int Vagas { get; set; }
    public int VagasOcupadas { get; set; }
    public bool PossuiVagas { get; set; }
    public bool PossuiPreRequisito { get; set; }
    public bool MatriculaPermitida { get; set; }
}
