using System.Collections.Generic;

namespace Cadernim.API.DTOs
{
    public class ReceitaInputDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public List<IngredienteInputDTO> Ingredientes { get; set; } = new List<IngredienteInputDTO>();
        public double PesoTotalDesejadoG { get; set; }
    }
}