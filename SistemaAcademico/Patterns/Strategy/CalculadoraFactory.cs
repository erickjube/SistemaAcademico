using SistemaAcademico.ENUMs;

namespace SistemaAcademico.Patterns.Strategy;

public class CalculadoraFactory
{
    public ICalculadoraMedia Criar(FormulaMediaTipo tipo)
    {
        return tipo switch
        {
            FormulaMediaTipo.Ponderada => new MediaPonderada(),
            FormulaMediaTipo.Aritmetica => new MediaAritmetica(),
            _ => throw new ArgumentOutOfRangeException(nameof(tipo), "Fórmula de média inválida.")
        };
    }
}
