using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Player;

namespace ConsoleUI
{
    public class Game
    {
        public Player Player { get; set; }
        public Game(Player Player)
        {
            this.Player = Player;
        }

        //main game loop
        public void Run()
        {

            string message = $"Class: { Player.Class } \n";
            message += $"Name: {Player.Name} \n";
            message += $"Weapon damage: {Player.EquippedWeapon.MinDamage} - {Player.EquippedWeapon.MaxDamage} \n";
            message += $"Gold: -- \n";
            message += $"Level: -- \n";
            message += $"Experience: --/--\n";
            message += $"_______________________\n";
            string[] options = { "Go on quest", "Duel", "Tournament", "Improve attributes" };
            Menu mainMenu = new Menu(options, message);
            int index = mainMenu.Run();

        }
    }
}
