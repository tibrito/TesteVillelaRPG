
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

namespace TesteVillelaRPG
{
    public class Personagem 
    {
        public string Nome { get; set; }
        public int Nivel { get; set; }
        public int atack { get; set; }
    }
    
    class program
    {
        static string gameState = "selection"
        static Personagem player = null;
        static Personagem enemy = null;

        static void main(string[] args)
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
        { }
            Console.Clear();
            Console.WriteLine("=== SELEÇÃO DE PERSONAGENS ===");

            var personagens = new List<Personagem>()
            {
                new Personagem { Nome = "Guerreiro", Vida = 100, Ataque = 20 },
                new Personagem { Nome = "Mago", Vida = 70, Ataque = 30 },
                new Personagem { Nome = "Arqueiro", Vida = 80, Ataque = 25 }
            };

          