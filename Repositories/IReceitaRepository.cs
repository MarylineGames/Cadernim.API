using System.Collections.Generic;
using System.Threading.Tasks;
using Cadernim.API.Models;

namespace Cadernim.API.Repositories
{
    public interface IReceitaRepository
    {
        Task<IEnumerable<Receita>> ObterTodasAsync();
        Task AdicionarAsync(Receita receita);
        Task<Receita?> ObterPorNomeAsync(string nome);
    }
}