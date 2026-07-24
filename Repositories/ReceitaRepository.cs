using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cadernim.API.Data;
using Cadernim.API.Models;

namespace Cadernim.API.Repositories
{
    public class ReceitaRepository : IReceitaRepository
    {
        private readonly AppDBContext _context;

        public ReceitaRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Receita>> ObterTodasAsync()
        {
            return await _context.Receitas.Include(r => r.Ingredientes).ToListAsync();
        }

        // Mude o nome para AdicionarAsync se estiver diferente, e limpe o corpo dele:
        public async Task AdicionarAsync(Receita receita)
        {
            await _context.Receitas.AddAsync(receita);
            await _context.SaveChangesAsync();
        }

        public async Task<Receita?> ObterPorNomeAsync(string nome)
        {
            return await _context.Receitas
                .Include(r => r.Ingredientes)
                .FirstOrDefaultAsync(r => r.Nome.ToLower() == nome.ToLower());
        }
    }
}