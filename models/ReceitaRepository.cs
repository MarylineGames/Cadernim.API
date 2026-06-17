using System.Collections.Generic;
using System.Linq;

namespace Cadernim.API.Models
{
    public static class ReceitaRepository
    {
        private static readonly List<Receita> _receitas = new List<Receita>();

        public static void AdicionarReceita(Receita receita)
        {
            _receitas.Add(receita);
        }

        public static List<Receita> ObterTodasReceitas()
        {
            return _receitas;
        }

        public static Receita? ObterReceitaPorNome(string nome)
        {
            return _receitas.FirstOrDefault(r => r.Nome.Equals(nome, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}