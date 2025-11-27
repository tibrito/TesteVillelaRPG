using System;
using System.Collections.Generic;

namespace ERPProdutos
{
    class Program
    {
        static List<string> produtos = new List<string>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ERP - Menu de Produtos ===");
                Console.WriteLine("1 - Adicionar Produto");
                Console.WriteLine("2 - Listar Produtos");
                Console.WriteLine("3 - Atualizar Produto");
                Console.WriteLine("4 - Remover Produto");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarProduto();
                        break;
                    case "2":
                        ListarProdutos();
                        break;
                    case "3":
                        AtualizarProduto();
                        break;
                    case "4":
                        RemoverProduto();
                        break;
                    case "0":
                        Console.WriteLine("Saindo...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void AdicionarProduto()
        {
            Console.Clear();
            Console.WriteLine("=== Adicionar Produto ===");

            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Preço: ");
            decimal preco = decimal.Parse(Console.ReadLine());

            Console.Write("Categoria: ");
            string categoria = Console.ReadLine();

            produtos.Add($"{nome};{preco};{categoria}");

            Console.WriteLine("\nProduto adicionado com sucesso!");
            Console.WriteLine("Pressione ENTER para voltar ao menu...");
            Console.ReadLine();
        }

        static void ListarProdutos()
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Produtos ===");

            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum produto cadastrado ainda!");
            }
            else
            {
                foreach (var p in produtos)
                {
                    Console.WriteLine(p);
                }
            }

            Console.WriteLine("\nPressione ENTER para voltar ao menu...");
            Console.ReadLine();
        }

        static void AtualizarProduto()
        {
            Console.Clear();
            Console.WriteLine("=== Atualizar Produto ===");

            Console.Write("Digite o nome do produto a atualizar: ");
            string nomeBusca = Console.ReadLine();

            int index = produtos.FindIndex(p => p.StartsWith(nomeBusca + ";"));

            if (index == -1)
            {
                Console.WriteLine("Produto não encontrado!");
                Console.ReadLine();
                return;
            }

            string[] dados = produtos[index].Split(';');

            Console.WriteLine("\nO que deseja alterar?");
            Console.WriteLine("1 - Nome");
            Console.WriteLine("2 - Preço");
            Console.WriteLine("3 - Categoria");
            Console.Write("Escolha: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.Write("Novo nome: ");
                    dados[0] = Console.ReadLine();
                    break;

                case "2":
                    Console.Write("Novo preço: ");
                    dados[1] = Console.ReadLine();
                    break;

                case "3":
                    Console.Write("Nova categoria: ");
                    dados[2] = Console.ReadLine();
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadLine();
                    return;
            }

            produtos[index] = string.Join(";", dados);

            Console.WriteLine("\nProduto atualizado com sucesso!");
            Console.ReadLine();
        }

        static void RemoverProduto()
        {
            Console.Clear();
            Console.WriteLine("=== Remover Produto ===");

            Console.Write("Digite o nome do produto a remover: ");
            string nomeBusca = Console.ReadLine();

            int index = produtos.FindIndex(p => p.StartsWith(nomeBusca + ";"));

            if (index == -1)
            {
                Console.WriteLine("Produto não encontrado!");
            }
            else
            {
                produtos.RemoveAt(index);
                Console.WriteLine("Produto removido com sucesso!");
            }

            Console.WriteLine("\nPressione ENTER para voltar ao menu...");
            Console.ReadLine();
        }
    }
}
