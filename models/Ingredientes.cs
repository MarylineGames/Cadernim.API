namespace Cadernim.API.Models
{
    public class Ingrediente
    {
        public string Nome { get; set; } = string.Empty;
        public double Porcentagem { get; set; } // Ex: 0.30 para 30%
        public double PesoCalculadoG { get; set; } // Onde vamos salvar o resultado em gramas
    }
}