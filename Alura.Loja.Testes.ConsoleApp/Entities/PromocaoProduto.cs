namespace Alura.Loja.Testes.ConsoleApp.Entities
{
    public class PromocaoProduto
    {
        public int ProdutosId { get; set; }
        public int PromocoesId { get; set; }
        public Produto Produtos { get; set; }
        public Promocao Promocoes { get; set; }

    }
}
