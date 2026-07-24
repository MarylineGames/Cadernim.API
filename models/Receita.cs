using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cadernim.API.Models
{
    public class Receita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        
        //FK Ingredientes
        public List<Ingrediente> Ingredientes { get; set; } = new List<Ingrediente>();
        
        public void CalcularPesos(double pesoTotalDesejadoG)
        {
            foreach (var ingrediente in Ingredientes)
            {
                // Multiplica o peso total (ex: 5000g) pela porcentagem do ingrediente (ex: 0.30)
                ingrediente.PesoCalculadoG = pesoTotalDesejadoG * ingrediente.Porcentagem;
            }
        }
    }
}