using System;
using GameLibrary;
using GameLibrary.Player;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Factory factory = new Factory();
            string[] options = new string[3] {"Warrior", "Scout", "Mage"};
            string message = "Choose one of following classes";
            //Console.WriteLine("Welcome!");
            //Console.WriteLine("What is your name?");
            Menu chooseClassMenu = new Menu(options, message);
            int i = chooseClassMenu.Run();
            Console.WriteLine("Choose one of following classes");
            
            
        }

        private static void Player_DamageReceived(object sender, DamageEventArgs e)
        {
            Console.WriteLine($"{e.Player.Name} received {e.DamageTaken} DMG , HP{e.Player.Health}");
        }
    }
}
