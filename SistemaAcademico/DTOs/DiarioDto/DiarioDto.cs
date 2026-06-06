namespace SistemaAcademico.DTOs.DiarioDto;

public class DiarioDto
{
    public int TurmaId { get; set; }
    public string Disciplina { get; set; }
    public string Professor { get; set; }
    public bool Fechada { get; set; }

    public List<AlunoDiarioDto> Alunos { get; set; } = [];
}
