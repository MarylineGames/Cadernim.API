using Microsoft.AspNetCore.Mvc;
using Cadernim.API.Repositories;
using Cadernim.API.Models;
using Cadernim.API.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Cadernim.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceitasController : ControllerBase
    {
        private readonly IReceitaRepository _receitaRepository;

        public ReceitasController(IReceitaRepository receitaRepository)
        {
            _receitaRepository = receitaRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CriarReceita([FromBody] ReceitaInputDTO input)
        {
            if (input == null || input.Ingredientes.Count == 0)
                return BadRequest("A receita precisa de ingredientes.");

            double somaPorcentagens = input.Ingredientes.Sum(i => i.Porcentagem);
            if (System.Math.Abs(somaPorcentagens - 1.0) > 0.001)
                return BadRequest("A soma das porcentagens deve ser exatamente 100%.");

            // Criamos a nova receita do absoluto zero (sem setar o Id, deixando ele começar em 0 para o banco preencher)
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

            // Chamamos o método do repositório
            await _receitaRepository.AdicionarAsync(novaReceita); 

            return Ok(novaReceita);
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodas()
        {
            // O Include garante que os ingredientes venham junto com a receita
            var receitas = await _receitaRepository.ObterTodasAsync();
            return Ok(receitas);
        }
    }
}