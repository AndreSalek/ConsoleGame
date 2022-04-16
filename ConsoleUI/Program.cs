using System;
using System.Collections.Generic;
using GameLibrary.Player;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Warrior player = new Warrior("Mark",40, 10, 10, 10);
            Warrior player1 = new Warrior("Bob", 35, 10, 10 , 10);
            player.DamageReceived += Player_DamageReceived;
            player.Duel(player1);
            Console.ReadKey();
        }

        private static void Player_DamageReceived(object sender, DamageEventArgs e)
        {
            Console.WriteLine($"{e.Player.Name} received {e.DamageTaken} DMG , HP{e.Player.Health}");
        }
    }
}
