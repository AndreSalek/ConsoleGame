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
                if (index == 0)
                {
                    Player player = factory.CreateWarrior(name);
                    Game game = new Game(player);
                    game.Run();
                }
                else if (index == 1)
                {
                    Player player = factory.CreateScout(name);
                    Game game = new Game(player);
                    game.Run();

                }
                else if (index == 2)
                {
                    Player player = factory.CreateMage(name);
                    Game game = new Game(player);
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
