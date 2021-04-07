using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alura.Loja.Testes.ConsoleApp.Entities;
using Microsoft.EntityFrameworkCore;
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

                var cliente = contexto.Clientes
                    .Include(e => e.EnderecoDeEntrega)
                    .FirstOrDefault();

                Console.WriteLine($"Endereço de entrega: {cliente.EnderecoDeEntrega.Logradouro}");



                var produto = contexto
                    .Produtos
                    //.Include(p => p.Compras)
                    .Where(p => p.Id == 3002)
                    .FirstOrDefault();




                contexto.Entry(produto)
                    .Collection(p => p.Compras)
                    .Query()
                    .Where(c => c.Preco <= 11).Load();

                

                Console.WriteLine($"\n\tCompras do produto: {produto.Nome}\n");

                foreach (var item in produto.Compras)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("=============");

                //var produtos = contexto.Compras
                //    .Include(c => c.Produto)
                //    .Where(p => p.Preco <= 10);

                //foreach (var item in produtos)
                //{
                //    Console.WriteLine(item.Produto);
                //}

                //var produtoCompra = contexto.Produtos.Where(p => p.Id == 3002).FirstOrDefault();

                //contexto.Compras.Add(new Compra(25, produtoCompra));

                //ExibeEntries(contexto.ChangeTracker.Entries());
                //contexto.SaveChanges();
                //ExibeEntries(contexto.ChangeTracker.Entries());
            }

            
            Console.WriteLine("Done");
            Console.ReadKey();

        }

        private static void ConsultaPromocao()
        {
            using (var contexto2 = new LojaContext())
            {
                var serviceProvider = contexto2.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                Console.WriteLine("\n==============================\n");
                Console.WriteLine("Produtos na promoção");

                var promocao = contexto2.Promocoes
                    .Include(p => p.PromocaoProduto)
                    .ThenInclude(pp => pp.Produtos)
                    .FirstOrDefault();

                foreach (var item in promocao.PromocaoProduto)
                {
                    Console.WriteLine(item.Produtos);
                }

            }
        }

        private static void IncluirPromocao()
        {
            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var promocao = new Promocao();
                promocao.Descricao = "Queima Total Julho 2021";
                promocao.DataInicio = new DateTime(2021, 07, 01);
                promocao.DataTermino = promocao.DataInicio.AddMonths(1);

                var produtos = contexto.Produtos.Where(p => p.Categoria == "Bebidas").ToList();

                foreach (var item in produtos)
                {
                    promocao.IncluiProduto(item);
                }
                contexto.Promocoes.Add(promocao);

                ExibeEntries(contexto.ChangeTracker.Entries());
                contexto.SaveChanges();
                ExibeEntries(contexto.ChangeTracker.Entries());

            }
        }

        private static void UmParaUm()
        {
            var cliente1 = new Cliente();
            cliente1.Nome = "João";
            cliente1.EnderecoDeEntrega = new Endereco()
            {
                Numero = 12,
                Logradouro = "Rua dos bobos",
                Bairro = "Jardim perdido",
                Complemento = "Casa",
                Cidade = "São Paulo"
            };

            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                contexto.Clientes.Add(cliente1);
                contexto.SaveChanges();

            }
        }

        private static void ExibeEntries(IEnumerable<EntityEntry> entries)
        {
            foreach (var e in entries)
            {
                Console.WriteLine(e.Entity.ToString() + " - " + e.State);
            }
        }

        private static void Compras()
        {

            //var paoFrances = new Produto();
            //paoFrances.Nome = "Pao Frances";
            //paoFrances.PrecoUnitario = 0.40;
            //paoFrances.Unidade = "Unidade";
            //paoFrances.Categoria = "Padaria";

            //var compra = new Compra();
            //compra.Quantidade = 6;
            //compra.Produto = paoFrances;
            //compra.Preco = paoFrances.PrecoUnitario * compra.Quantidade;

            //var paoFrances2 = new Produto();
            //paoFrances2.Nome = "Pao Frances";
            //paoFrances2.PrecoUnitario = 0.40;
            //paoFrances2.Unidade = "Unidade";
            //paoFrances2.Categoria = "Padaria";

            //var compra2 = new Compra();
            //compra2.Quantidade = 15;
            //compra2.Produto = paoFrances2;
            //compra2.Preco = paoFrances2.PrecoUnitario * compra2.Quantidade;

            //var paoFrances3 = new Produto();
            //paoFrances3.Nome = "Pao Frances";
            //paoFrances3.PrecoUnitario = 0.40;
            //paoFrances3.Unidade = "Unidade";
            //paoFrances3.Categoria = "Padaria";

            //var compra3 = new Compra();
            //compra3.Quantidade = 20;
            //compra3.Produto = paoFrances3;
            //compra3.Preco = paoFrances3.PrecoUnitario * compra3.Quantidade;

            using (var contexto = new LojaContext())
            {
                //contexto.Compras.Add(compra);
                //contexto.Compras.Add(compra2);
                //contexto.Compras.Add(compra3);
                //contexto.SaveChanges();
            }

        }

        private static void MuitosParaMuitos()
        {
            //var paoFrances = new Produto();
            //paoFrances.Nome = "Pao Frances";
            //paoFrances.PrecoUnitario = 0.40;
            //paoFrances.Unidade = "Unidade";
            //paoFrances.Categoria = "Padaria";

            //var compra = new Compra();
            //compra.Quantidade = 6;
            //compra.Produto = paoFrances;
            //compra.Preco = paoFrances.PrecoUnitario * compra.Quantidade;
            var p1 = new Produto();
            p1.Nome = "Suco de Laranja";
            p1.Categoria = "Bebidas";
            p1.PrecoUnitario = 8.79;
            p1.Unidade = "Litros";

            var p2 = new Produto() { Nome = "Café", Categoria = "Bebida", PrecoUnitario = 12.45, Unidade = "Gramas" };
            var p3 = new Produto() { Nome = "Macarrão", Categoria = "Alimento", PrecoUnitario = 4.23, Unidade = "Gramas" };

            var promocaoDePascoa = new Promocao();
            promocaoDePascoa.Descricao = "Páscoa Feliz";
            promocaoDePascoa.DataInicio = DateTime.Now;
            promocaoDePascoa.DataTermino = DateTime.Now.AddMonths(3);


            promocaoDePascoa.IncluiProduto(p1);
            promocaoDePascoa.IncluiProduto(p2);
            promocaoDePascoa.IncluiProduto(p3);




            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                //contexto.Promocoes.Add(promocaoDePascoa);
                var promocao = contexto.Promocoes.Find(1);

                contexto.Promocoes.Remove(promocao);

                ExibeEntries(contexto.ChangeTracker.Entries());

                contexto.SaveChanges();
            }

        }
    }
}
