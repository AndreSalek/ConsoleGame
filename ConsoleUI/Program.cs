using System;
using GameLibrary;
using GameLibrary.Player;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Factory factory = new Factory();
                string[] options = new string[3] { "Warrior", "Scout", "Mage" };
                string message = "Choose one of following classes";
                Console.WriteLine("Welcome!");
                Console.WriteLine("What is your name?");
                string name = Console.ReadLine();
                Menu classMenu = new Menu(options, message);
                int index = classMenu.Run();
                /*creating opponents here because I want player to be last ranking
                  at start so he needs to be intantiated later*/
                factory.CreateOpponents(20);
                if (index == 0)
                {
                    PlayerModel player = factory.CreateWarrior(name);
                    Game game = new Game(player, factory);
                    game.Run();
                }
                else if (index == 1)
                {
                    PlayerModel player = factory.CreateScout(name);
                    Game game = new Game(player, factory);
                    game.Run();

                }
                else if (index == 2)
                {
                    PlayerModel player = factory.CreateMage(name);
                    Game game = new Game(player, factory);
                    game.Run();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception \n"+ ex);
            }
            
        }

        private static void Player_DamageReceived(object sender, DamageEventArgs e)
        {
            Console.WriteLine($"{e.Player.Name} received {e.DamageTaken} DMG , HP{e.Player.Health}");
        }
    }
}
