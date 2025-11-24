
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

namespace testevillelarpg
{
    public class Personagem
    {
        public string Nome { get; set; }
        public int Vida { get; set; }
        public int Ataque { get; set; }
        //c
    }

    class Program
    {
        static string gameState = "selection";
        static Personagem player = null;
        static Personagem enemy = null;

        static void Main(string[] args)
        {
            while (true)
            {
                if (gameState == "selection")
                {
                    CharacterSelection();
                }
                else if (gameState == "battle" && player != null && enemy != null)
                {
                    BattleArena();
                }
            }
        }

        static void CharacterSelection()
        {
            Console.Clear();
            Console.WriteLine("=== SELEÇÃO DE PERSONAGENS ===");

            var personagens = new List<Personagem>()
            {
                new Personagem { Nome = "Guerreiro", Vida = 100, Ataque = 20 },
                new Personagem { Nome = "Mago", Vida = 70, Ataque = 30 },
                new Personagem { Nome = "Arqueiro", Vida = 80, Ataque = 25 }
            };

            Console.WriteLine("\nEscolha seu personagem:");
            for (int i = 0; i < personagens.Count; i++)
                Console.WriteLine($"{i + 1} - {personagens[i].Nome}");

            Console.Write("\nDigite o número: ");
            int escolha = int.Parse(Console.ReadLine()) - 1;

            player = personagens[escolha];

            Console.WriteLine($"\nVocê escolheu: {player.Nome}");

            Random rnd = new Random();
            do
            {
                enemy = personagens[rnd.Next(personagens.Count)];
            } while (enemy.Nome == player.Nome);

            Console.WriteLine($"Seu inimigo será: {enemy.Nome}");

            Console.WriteLine("\nPressione ENTER para iniciar a batalha...");
            Console.ReadLine();

            gameState = "battle";
        }

        static void BattleArena()
        {
            Console.Clear();
            Console.WriteLine("=== BATALHA ===");

            while (player.Vida > 0 && enemy.Vida > 0)
            {
                Console.WriteLine($"\n{player.Nome} ataca causando {player.Ataque} de dano!");
                enemy.Vida -= player.Ataque;

                if (enemy.Vida <= 0) break;

                Console.WriteLine($"{enemy.Nome} agora tem {enemy.Vida} de vida.");

                Console.WriteLine($"\n{enemy.Nome} ataca causando {enemy.Ataque} de dano!");
                player.Vida -= enemy.Ataque;

                Console.WriteLine($"{player.Nome} agora tem {player.Vida} de vida.");

                Console.WriteLine("\nPressione ENTER para continuar...");
                Console.ReadLine();
            }

            if (player.Vida > 0)
                Console.WriteLine($"\n🎉 {player.Nome} venceu a batalha!");
            else
                Console.WriteLine($"\n💀 {enemy.Nome} venceu a batalha!");

            Console.WriteLine("\nPressione ENTER para reiniciar...");
            Console.ReadLine();

            handleRestart();
        }

        static void handleRestart()
        {
            player = null;
            enemy = null;
            gameState = "selection";
        }
    }
}
