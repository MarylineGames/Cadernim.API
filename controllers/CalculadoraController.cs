using Microsoft.AspNetCore.Mvc;
//Minhas classes de modelo e DTOs ^v^-
using Cadernim.API.Models;
using Cadernim.API.DTOs;
using System.Linq;

namespace Cadernim.API.Controllers
{
    //Necessários para criar as rotas
    [ApiController]
    [Route("api/[controller]")]
    public class CalculadoraController : ControllerBase
    {
        [HttpPost("escalar")]
        public IActionResult EscalarReceita([FromBody] ReceitaInputDTO input)
        {
            if (input == null || input.Ingredientes.Count == 0)
            {
                return BadRequest("A receita deve conter pelo menos um ingrediente.");
            }
            if (input.PesoTotalDesejadoG <= 0)
            {
                return BadRequest("O peso total desejado deve ser maior que zero.");
            }

            double totalPorcentagem = input.Ingredientes.Sum(i => i.Porcentagem);
            if (System.Math.Abs(totalPorcentagem - 1.0) > 0.001)
            {
                return BadRequest("A soma das porcentagens dos ingredientes deve ser igual a 100%.");
            }

            var receita = new Receita
            {
                Nome = input.Nome,
                Descricao = input.Descricao,
                Ingredientes = input.Ingredientes.Select(i => new Ingrediente
                {
                    Nome = i.Nome,
                    Porcentagem = i.Porcentagem
                }).ToList()
            };

            receita.CalcularPesos(input.PesoTotalDesejadoG);

            return Ok(receita);
        }

        // Rota para calcular os pesos dos ingredientes de uma receita existente por nome
        [HttpGet("calcular-por-nome")]
        public IActionResult CalcularPorNome([FromQuery] string nomeReceita, [FromQuery] double pesoTotalDesejadoG)
        {
            // Validações básicas
            if (string.IsNullOrWhiteSpace(nomeReceita))
            {
                return BadRequest("O nome da receita é obrigatório.");
            }
            if (pesoTotalDesejadoG <= 0)
            {
                return BadRequest("O peso total desejado deve ser maior que zero.");
            }

            // Busca a receita pelo nome no repositório
            var receita = ReceitaRepository.ObterReceitaPorNome(nomeReceita);
            if (receita == null)
            {
                return NotFound($"Receita '{nomeReceita}' não encontrada.");
            }

            // Cópia para não alterar a receita original
            Receita receitaCalculada = new Receita
            {
                Nome = receita.Nome,
                Descricao = receita.Descricao,
                Ingredientes = receita.Ingredientes.Select(i => new Ingrediente
                {
                    Nome = i.Nome,
                    Porcentagem = i.Porcentagem
                }).ToList()
            };

            receitaCalculada.CalcularPesos(pesoTotalDesejadoG);
            return Ok(receitaCalculada);
        }
    }
}