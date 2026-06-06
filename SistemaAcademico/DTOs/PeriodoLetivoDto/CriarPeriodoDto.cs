namespace SistemaAcademico.DTOs.PeriodoLetivoDto;

public class CriarPeriodoDto
{
    public string Semestre { get; set; }
    public DateTime InicioMatricula { get; set; }
    public DateTime FimMatricula { get; set; }
    public DateTime InicioAulas { get; set; }
    public DateTime FimAulas { get; set; }
}
