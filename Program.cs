using System;
using System.Collections.Generic;

namespace testevillelarpg
{
    public class Personagem
    {
        public string Nome { get; set; }
        public int Vida { get; set; }
        public int Ataque { get; set; }

        // skill ataque especial
        public int CooldownEspecial { get; set; } = 0;
        public int Defesa { get; internal set; }
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
                new Personagem { Nome = "Guerreiro", Vida = 100, Ataque = 35, Defesa = 15},
                new Personagem { Nome = "Mago", Vida = 100, Ataque = 45, Defesa = 5 },
                new Personagem { Nome = "Arqueiro", Vida = 100, Ataque =42, Defesa = 8},
            };

            Console.WriteLine("\nEscolha seu personagem:");
            for (int i = 0; i < personagens.Count; i++)
                Console.WriteLine($"{i + 1} - {personagens[i].Nome}");

            Console.Write("\nDigite o número: ");
            string? input = Console.ReadLine();
            if (!int.TryParse(input, out int escolha) || escolha < 1 || escolha > personagens.Count)
            {
                Console.WriteLine("Escolha inválida! Pressione ENTER para tentar novamente...");
                Console.ReadLine();
                return;
            }
            escolha -= 1;

            player = personagens[escolha];

            Random rnd = new Random();
            do
            {
                enemy = personagens[rnd.Next(personagens.Count)];
            } while (enemy.Nome == player.Nome);

            Console.WriteLine($"\nVocê escolheu: {player.Nome}");
            Console.WriteLine($"Seu inimigo será: {enemy.Nome}");

            Console.WriteLine("\nPressione ENTER para iniciar a batalha...");
            Console.ReadLine();

            gameState = "battle";
        }

        static void BattleArena()
        {
            Console.Clear();

            while (player.Vida > 0 && enemy.Vida > 0)
            {
                Console.Clear();
                Console.WriteLine("=== BATALHA ===\n");
                Console.WriteLine($"{player.Nome} - Vida: {player.Vida} | Cooldown Especial: {player.CooldownEspecial}");
                Console.WriteLine($"{enemy.Nome} - Vida: {enemy.Vida}");
                Console.WriteLine("\nEscolha sua ação:");

                Console.WriteLine("(1) Ataque Normal");

                if (player.CooldownEspecial == 0)
                    Console.WriteLine("(2) Ataque Especial (+15% dano)");
                else
                    Console.WriteLine("(2) Ataque Especial (INDISPONÍVEL)");

                Console.Write("\nDigite sua escolha: ");
                string escolha = Console.ReadLine();

                // Turno do jogador
                if (escolha == "1")
                {
                    Console.WriteLine($"\n{player.Nome} usa ATAQUE NORMAL!");
                    enemy.Vida -= player.Ataque;
                }
                else if (escolha == "2" && player.CooldownEspecial == 0)
                {
                    int danoEspecial = (int)(player.Ataque * 1.15);
                    Console.WriteLine($"\n{player.Nome} usa ATAQUE ESPECIAL causando {danoEspecial} de dano!");
                    enemy.Vida -= danoEspecial;

                    player.CooldownEspecial = 2; // leva 2 turnos para recarregar
                }
                else
                {
                    Console.WriteLine("\nAção inválida! Você perde o turno!");
                }

                // Verifica se o inimigo morreu
                if (enemy.Vida <= 0) break;

                // Turno do inimigo
                Console.WriteLine($"\n{enemy.Nome} ataca causando {enemy.Ataque} de dano!");
                player.Vida -= enemy.Ataque;

                // Atualiza cooldown
                if (player.CooldownEspecial > 0)
                    player.CooldownEspecial--;

                // Verifica morte do jogador
                if (player.Vida <= 0) break;

                Console.WriteLine("\nPressione ENTER para continuar o próximo turno...");
                Console.ReadLine();
            }

            // Resultado final
            Console.Clear();
            if (player.Vida > 0)
                Console.WriteLine($"🎉 {player.Nome} venceu a batalha!");
            else
                Console.WriteLine($"💀 {enemy.Nome} venceu a batalha!");

            Console.WriteLine("\nPressione ENTER para reiniciar...");
            Console.ReadLine();
            Restart();
        }

        static void Restart()
        {
            player = null;
            enemy = null;
            gameState = "selection";
        }
    }
}
