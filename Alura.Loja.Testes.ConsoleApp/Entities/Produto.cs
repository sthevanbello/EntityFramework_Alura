using Alura.Loja.Testes.ConsoleApp.Entities;
using System.Collections.Generic;

namespace Alura.Loja.Testes.ConsoleApp
{
    public class Produto
    {
        public int Id { get; internal set; }
        public string Nome { get; internal set; }
        public string Categoria { get; internal set; }
        public double PrecoUnitario { get; internal set; }
        public string Unidade { get; internal set; }
        public IList<PromocaoProduto> PromocaoProduto { get; internal set; }
        public IList<Compra> Compras { get; set; }


        public Produto()
        {

        }
        public override string ToString()
        {
            return $"{Nome}, Id: {Id}, {Categoria}, {PrecoUnitario}";
        }
    }
}