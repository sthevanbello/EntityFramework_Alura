using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var contexto = new LojaContext())
            {

                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var listaProdutos = contexto.Produtos.ToList();

                ExibeEntries(contexto.ChangeTracker.Entries());

                var produto1 = new Produto();
                produto1.Nome = "A nuvem da morte";
                produto1.Categoria = "Livro";
                produto1.Preco = 40.00;

                contexto.Produtos.Add(produto1);

                ExibeEntries(contexto.ChangeTracker.Entries());

                contexto.SaveChanges();

                ExibeEntries(contexto.ChangeTracker.Entries());
            }
            Console.ReadKey();
        }

        private static void ExibeEntries(IEnumerable<EntityEntry> contexto)
        {
            Console.WriteLine("============================================");
            foreach (var item in contexto)
            {
                Console.WriteLine(item.State);
                Console.WriteLine(item.Entity.ToString());
            }
        }


        #region Testes Parte 1
        //private static void GravarUsandoEntity()
        //{

        //    using (var contexto = new ProdutoDAOEntity())
        //    {
        //        Produto p1 = new Produto();
        //        p1.Nome = "Harry Potter e a Ordem da Fênix";
        //        p1.Categoria = "Livros";
        //        p1.Preco = 19.89;

        //        Produto p2 = new Produto();
        //        p2.Nome = "Senhor dos Anéis 1";
        //        p2.Categoria = "Livros";
        //        p2.Preco = 19.89;

        //        Produto p3 = new Produto();
        //        p3.Nome = "O Monge e o Executivo";
        //        p3.Categoria = "Livros";
        //        p3.Preco = 19.89;

        //        contexto.Adicionar(p1, p2, p3);

        //    }
        //    Console.WriteLine("Done...");

        //}

        //private static void GravarUsandoAdoNet()
        //{
        //    Produto p = new Produto();
        //    p.Nome = "Harry Potter e a Ordem da Fênix";
        //    p.Categoria = "Livros";
        //    p.Preco = 19.89;

        //    using (var repo = new ProdutoDAO())
        //    {
        //        repo.Adicionar(p);
        //    }
        //}

        //private static void RecuperaProdutos()
        //{
        //    using (var repo = new ProdutoDAOEntity())
        //    {
        //        List<Produto> produtos = repo.Produtos();

        //        var orderList = produtos.OrderBy(p => p.Nome);

        //        Console.WriteLine($"Foram encontrados {produtos.Count} produtos");

        //        foreach (var item in orderList)
        //        {
        //            Console.WriteLine($"\n{item.Nome}\n{item.Preco:C}");
        //        }
        //    }
        //}

        //private static void RemoverProdutos()
        //{
        //    using (var repo = new ProdutoDAOEntity())
        //    {
        //        List<Produto> produtos = repo.Produtos();

        //        foreach (var item in produtos)
        //        {
        //            repo.Remover(item);
        //        }

        //    }
        //}

        //private static void AtualizarProduto()
        //{
        //    // inclui um produto
        //    //GravarUsandoEntity();
        //    RecuperaProdutos();

        //    // atualiza o produto
        //    using (var repo = new ProdutoDAOEntity())
        //    {
        //        List<Produto> produtos = repo.Produtos();

        //        foreach (var produto in produtos)
        //        {
        //            if (produto.Nome == "Senhor dos Anéis 1")
        //            {
        //                produto.Nome = "O Senhor dos Anéis - A sociedade do Anel";

        //                repo.Atualizar(produto);

        //            }
        //        }

        //    }
        //    RecuperaProdutos();
        //}

        #region Atualizar antigo
        //private static void AtualizarProduto()
        //{
        //    // inclui um produto
        //    //GravarUsandoEntity();
        //    RecuperaProdutos();

        //    // atualiza o produto
        //    using (var repo = new LojaContext())
        //    {
        //        List<Produto> produtos = repo.Produtos.ToList();

        //        foreach (var produto in produtos)
        //        {
        //            if (produto.Nome == "Senhor dos Anéis 1")
        //            {
        //                produto.Nome = "O Senhor dos Anéis - A sociedade do Anel";

        //                repo.Produtos.Update(produto);

        //            }
        //        }
        //        repo.SaveChanges();
        //    }
        //    RecuperaProdutos();
        //}
        #endregion

        #endregion
    }
}

