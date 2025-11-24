using System;
using System.Collections.Generic;

namespace testevillelarpg
{
    public class Personagem
    {
        public string Nome { get; set; }
        public int Vida { get; set; }
        public int Ataque { get; set; }
        public int Defesa { get; set; }

        public int CooldownEspecial { get; set; } = 0;
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
                    CharacterSelection();

                else if (gameState == "battle" && player != null && enemy != null)
                    BattleArena();
            }
        }

        static void CharacterSelection()
        {
            Console.Clear();
            Console.WriteLine("=== SELEÇÃO DE PERSONAGENS ===");

            var personagens = new List<Personagem>()
            {
                new Personagem { Nome = "Guerreiro", Vida = 150, Ataque = 30, Defesa = 20 },
                new Personagem { Nome = "Mago", Vida = 100, Ataque = 45, Defesa = 8 },
                new Personagem { Nome = "Arqueiro", Vida = 120, Ataque = 35, Defesa = 12 }
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

            player = personagens[escolha - 1];

            // Escolha do inimigo
            Console.Clear();
            Console.WriteLine("=== SELEÇÃO DO INIMIGO ===\n");

            for (int i = 0; i < personagens.Count; i++)
                if (personagens[i].Nome != player.Nome)
                    Console.WriteLine($"{i + 1} - {personagens[i].Nome}");

            Console.Write("\nDigite o número do inimigo: ");
            input = Console.ReadLine();

            if (!int.TryParse(input, out escolha) || escolha < 1 || escolha > personagens.Count ||
                personagens[escolha - 1].Nome == player.Nome)
            {
                Console.WriteLine("Escolha inválida! Pressione ENTER para tentar novamente...");
                Console.ReadLine();
                return;
            }

            enemy = personagens[escolha - 1];

            Console.WriteLine($"\nVocê escolheu: {player.Nome}");
            Console.WriteLine($"O adversário será: {enemy.Nome}");

            Console.WriteLine("\nPressione ENTER para iniciar a batalha...");
            Console.ReadLine();

            gameState = "battle";
        }

        static int CalcularDano(Personagem atacante, Personagem alvo)
        {
            int dano = atacante.Ataque - (alvo.Defesa / 2);
            if (dano < 1) dano = 1; // nunca zero
            return dano;
        }

        static void BattleArena()
        {
            Console.Clear();

            while (player.Vida > 0 && enemy.Vida > 0)
            {
                Console.Clear();
                Console.WriteLine("=== BATALHA ===\n");
                Console.WriteLine($"{player.Nome} - Vida: {player.Vida} | Defesa: {player.Defesa} | Cooldown: {player.CooldownEspecial}");
                Console.WriteLine($"{enemy.Nome} - Vida: {enemy.Vida} | Defesa: {enemy.Defesa}");
                Console.WriteLine("\nEscolha sua ação:");

                Console.WriteLine("(1) Ataque Normal");

                if (player.CooldownEspecial == 0)
                    Console.WriteLine("(2) Ataque Especial (+15% dano)");
                else
                    Console.WriteLine("(2) Ataque Especial (INDISPONÍVEL)");

                Console.Write("\nDigite sua escolha: ");
                string escolha = Console.ReadLine();

                // --- TURNO DO JOGADOR ---
                if (escolha == "1")
                {
                    int dano = CalcularDano(player, enemy);
                    Console.WriteLine($"\n{player.Nome} usa ATAQUE NORMAL causando {dano} de dano!");
                    enemy.Vida -= dano;
                }
                else if (escolha == "2" && player.CooldownEspecial == 0)
                {
                    int danoBase = (int)(player.Ataque * 1.15);
                    int danoFinal = danoBase - (enemy.Defesa / 2);
                    if (danoFinal < 1) danoFinal = 1;

                    Console.WriteLine($"\n{player.Nome} usa ATAQUE ESPECIAL causando {danoFinal} de dano!");
                    enemy.Vida -= danoFinal;

                    player.CooldownEspecial = 2;
                }
                else
                {
                    Console.WriteLine("\nAção inválida! Você perde o turno!");
                }

                if (enemy.Vida <= 0) break;

                // --- TURNO DO INIMIGO ---
                int danoInimigo = CalcularDano(enemy, player);
                Console.WriteLine($"\n{enemy.Nome} ataca causando {danoInimigo} de dano!");
                player.Vida -= danoInimigo;

                if (player.CooldownEspecial > 0)
                    player.CooldownEspecial--;

                if (player.Vida <= 0) break;

                Console.WriteLine("\nPressione ENTER para continuar o próximo turno...");
                Console.ReadLine();
            }

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
