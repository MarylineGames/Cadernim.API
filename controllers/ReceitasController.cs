using Microsoft.AspNetCore.Mvc;
using Cadernim.API.Models;
using Cadernim.API.DTOs;
using System.Linq;

namespace Cadernim.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceitasController : ControllerBase
    {
        // 1. Rota para salvar uma receita no livro (POST api/receitas)
        [HttpPost]
        public IActionResult CriarReceita([FromBody] ReceitaInputDTO input)
        {
            if (input == null || input.Ingredientes.Count == 0)
            {
                return BadRequest("A receita deve conter pelo menos um ingrediente.");
            }

            // Verificação de negócio: A soma das porcentagens dos ingredientes deve ser igual a 100%
            double totalPorcentagens = input.Ingredientes.Sum(i => i.Porcentagem);
            if (System.Math.Abs(totalPorcentagens - 1.0) > 0.001)
            {
                return BadRequest("A soma das porcentagens dos ingredientes deve ser igual a 100%.");
            }

            var novaReceita = new Receita
            {
                Nome = input.Nome,
                Descricao = input.Descricao,
                Ingredientes = input.Ingredientes.Select(i => new Ingrediente
                {
                    Nome = i.Nome,
                    Porcentagem = i.Porcentagem
                }).ToList()
            };

            // Memória provisória: Adiciona a receita à lista estática do repositório
            ReceitaRepository.AdicionarReceita(novaReceita);
            return Ok($"Receita criada com sucesso: {novaReceita.Nome}");
        }

        // 2. Rota para obter uma receita por nome (GET api/receitas/{nome})
        [HttpGet("{nome}")]
        public IActionResult ObterReceita(string nome)
        {
            var receita = ReceitaRepository.ObterReceitaPorNome(nome);
            if (receita == null)
            {
                return NotFound();
            }
            return Ok(receita);
        }

        // 3. Rota para listar todas as receitas (GET api/receitas)
        [HttpGet]
        public IActionResult ObterTodasReceitas()
        {
            var receitas = ReceitaRepository.ObterTodasReceitas();
            return Ok(receitas);
        }
    }
}