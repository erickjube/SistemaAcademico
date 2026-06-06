namespace SistemaAcademico.DTOs.DisciplinaDto;

public class UpdateDisciplinaDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int CargaHoraria { get; set; }
    public int? PreRequisitoId { get; set; }
}
