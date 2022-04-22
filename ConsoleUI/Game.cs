using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary;
using GameLibrary.Interactions;
using GameLibrary.Player;

namespace ConsoleUI
{
    public class Game
    {
        public PlayerModel player;

        private static Logger log = new Logger();
        private Interaction interaction = new Interaction(log);
        private Factory factory = new Factory();
        public Game(PlayerModel player)
        {
            this.player = player;
        }

        //main game loop
        public void Run()
        {

            string message = $"Class: { player.Class } \n";
            message += $"Name: {player.Name} \n";
            message += $"Weapon damage: {player.EquippedWeapon.MinDamage} - {player.EquippedWeapon.MaxDamage} \n";
            message += $"Gold: -- \n";
            message += $"Level: -- \n";
            message += $"Experience: --/--\n";
            message += $"_______________________\n";
            string[] options = { "Go on quest", "Duel", "Tournament", "Improve attributes" };
            Menu mainMenu = new Menu(options, message);
            int index = mainMenu.Run();

            switch(index)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;

            }
        }
    }
}
