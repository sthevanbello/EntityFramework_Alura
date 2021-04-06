using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp.Entities
{
    public class Promocao
    {
        

        public int Id { get; set; }
        public string Descricao { get; internal set; }
        public DateTime DataInicio { get; internal set; }
        public DateTime DataTermino { get; internal set; }
        public IList<PromocaoProduto> PromocaoProduto { get; set; } = new List<PromocaoProduto>();

        public Promocao()
        {
            
        }

        public void IncluiProduto(Produto produto)
        {
            PromocaoProduto.Add(new PromocaoProduto()
            {
                Produtos = produto
            });
        }
    }
}
