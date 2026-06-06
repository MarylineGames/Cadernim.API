using Microsoft.AspNetCore.Mvc;
using Cadernim.API.Models;

namespace Cadernim.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculadoraController : ControllerBase
    {
        [HttpPost("escalar")]
        public IActionResult EscalarReceita([FromQuery] Receita receita, [FromQuery] double pesoTotalDesejadoG)
        {
            if (receita == null || receita.Ingredientes.Count == 0)
            {
                return BadRequest("A receita deve conter pelo menos um ingrediente.");
            }

            if (pesoTotalDesejadoG <= 0)
            {
                return BadRequest("O peso total desejado deve ser maior que zero.");
            }

            receita.CalcularPesos(pesoTotalDesejadoG);
            return Ok(receita);
        }
    }
}