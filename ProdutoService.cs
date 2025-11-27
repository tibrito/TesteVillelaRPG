using System;
using System.Collections.Generic;
using System.Linq;

namespace ERP26._1123hrs
{
    public class ProdutoService
    {
        public void AdicionarProdutoDireto(Produto produto)
        {
            Produtos.Add(produto);
        }

        public List<Produto> Produtos { get; private set; } = new List<Produto>();

        public void ListarProdutos()
        {
            if (Produtos.Count == 0)
            {
                Console.WriteLine("Nenhum produto cadastrado.");
                return;
            }

            Console.WriteLine("\n=== LISTA DE PRODUTOS ===");
            foreach (var p in Produtos)
            {
                Console.WriteLine(p);
            }
        }
    }
}
