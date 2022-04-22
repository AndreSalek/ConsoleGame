using System;
using GameLibrary.Player;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Interactions
{
    //this class will contain all actions that player can do (quests,duels etc.)
    public class Interaction
    {
        Logger log;
        public Interaction(Logger log)
        {
            this.log = log;
        }
        //Simulates 1v1 battle between two players
        //Battle is divided into turns
        //1 turn is when both players had a chance to attack
        public PlayerModel Duel(PlayerModel player, PlayerModel player1)
        {
            PlayerModel winner;
            int turn = 0;
            var firstAttack = new Random().Next(0, 100);
            bool attackerFirst = (firstAttack <= 50) ? true : false;
            while (player.Health > 0 && player1.Health > 0)
            {
                ++turn;
                Console.WriteLine($"Turn { turn} started");
                //method SimulateTurn makes player that is passed first as parameter attack first
                if (attackerFirst) SimulateTurn(player, player1);
                else SimulateTurn(player1, player);
            }
            if (player.Health == 0) winner = player1;
            else winner = player;
            player.RestoreHealth();
            player1.RestoreHealth();

            return winner;
        }

        //simulates 1 turn fight between 2 players
        //1 turn is passed when both players attack
        public void SimulateTurn(PlayerModel player, PlayerModel player1)
        {
            int dmg;
            dmg = new Random().Next(player.MinDamage, player.MaxDamage);
            player1.ReceiveDamage(player, dmg);
            if (player.Health == 0 || player1.Health == 0) return;
            dmg = new Random().Next(player1.MinDamage, player1.MaxDamage);
            player.ReceiveDamage(player1, dmg);
        }
    }
}
