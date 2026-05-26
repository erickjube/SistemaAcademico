namespace SistemaAcademico.Patterns.Strategy;

public class MediaAritmetica : ICalculadoraMedia
{
    public decimal Calcular(decimal p1, decimal p2, decimal trabalho)
        => (p1 + p2 + trabalho) / 3m;
}
