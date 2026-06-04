namespace SistemaAcademico.DTOs.DisciplinaDto;

public class DisciplinaDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int CursoId { get; set; }
    public int CargaHoraria { get; set; }
    public int? PreRequisitoId { get; set; }
}
