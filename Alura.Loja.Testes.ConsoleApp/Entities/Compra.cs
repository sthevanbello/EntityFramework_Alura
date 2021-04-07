namespace Alura.Loja.Testes.ConsoleApp
{
    public class Compra
    {
        public int Id { get; set; }
        public int Quantidade { get; internal set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; internal set; }
        public double Preco { get; internal set; }

        public Compra(int quantidade, Produto produto)
        {
            Quantidade = quantidade;
            Produto = produto;
            Preco = PrecoTotal();
        }
        public Compra()
        {

        }

        private double PrecoTotal()
        {
           return this.Produto.PrecoUnitario * Quantidade;
        }

        public override string ToString()
        {
            return $"Compra de: {Produto} - Preço total: {PrecoTotal():C}";
        }
    }
}