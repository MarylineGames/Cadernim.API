using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cadernim.API.Models
{
    public class Ingrediente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;
        public double Porcentagem { get; set; } // Ex: 0.30 para 30%
        public double PesoCalculadoG { get; set; } // Onde vamos salvar o resultado em gramas

        //FK Receita
        public int ReceitaId { get; set; }
    }
}