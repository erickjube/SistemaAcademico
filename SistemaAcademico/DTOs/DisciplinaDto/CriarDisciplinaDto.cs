namespace SistemaAcademico.DTOs.DisciplinaDto;

public class CriarDisciplinaDto
{
    public string Nome { get; set; } = string.Empty;
    public int CursoId { get; set; }
    public int CargaHoraria { get; set; }
    public int? PreRequisitoId { get; set; }
}
