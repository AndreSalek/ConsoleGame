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
        public string fightLogs;
        PlayerModel mainPlayer;             //for fight log purposes
        private bool isMainPlayerFight = true;

        public Interaction(PlayerModel mainPlayer)
        {
            this.mainPlayer = mainPlayer;
        }

        //Simulates 1v1 battle between two players
        //Battle is divided into turns
        //1 turn is when both players had a chance to attack
        public PlayerModel Duel(PlayerModel player, PlayerModel player1)
        {
            PlayerModel winner;
            int turn = 0;
            var firstAttackDecision = new Random().Next(0, 100);
            bool attackFirstToken = (firstAttackDecision <= 50) ? true : false;
            if (isMainPlayerFight)
            {
                player.DamageReceived += Player_DamageReceived;
                player1.DamageReceived += Opponent_DamageReceived;
            }

                while (player.Health > 0 && player1.Health > 0)
            {
                ++turn;
                if (isMainPlayerFight) fightLogs += $"Turn {turn} started\n";
                //method SimulateTurn makes player that is passed first as parameter attack first
                if (attackFirstToken) SimulateTurn(player, player1);
                else SimulateTurn(player1, player);
            }
            if (player.Health == 0) winner = player1;
            else winner = player;

            player.DamageReceived -= Player_DamageReceived;
            player1.DamageReceived -= Opponent_DamageReceived;
            
            player.RestoreHealth();
            player1.RestoreHealth();
            return winner;
        }

        //returns winner of the tournament
        public PlayerModel Tournament(List<PlayerModel> players)
        {
            int playerCount = players.Count();
            if (!(playerCount % 2 == 0)) return null;                  //Tournament will start only when players count is even
            List<int> roundLoserIndex = new List<int>();
            int cycles = 0;
            int fightNumber = 0;
            //Count how many rounds needs to happen until one PlayerModel is left
            while (playerCount > 1)
            {
                playerCount /= 2;
                cycles += 1;
            }
            //repeat fighting until variable cycles reaches 0
            for(int i = cycles; i > 0; i--)
            {
                //duel until every PlayerModel had it's duel
                //Add loosers index in players list to roundLoserIndex
                for (int j = 0; j < players.Count()/2; j++)
                {
                    PlayerModel fighter1 = players[j];
                    PlayerModel fighter2 = players[players.Count() - 1 - j];
                    PlayerModel duelWinner;
                    if (fighter1 == mainPlayer || fighter2 == mainPlayer)
                    {
                        fightNumber++;
                        fightLogs += $"Fight {fightNumber} start \n_______________________\n";
                        isMainPlayerFight = true;
                        duelWinner = Duel(fighter1, fighter2);
                        fightLogs += "_______________________\n";
                    }
                    else
                    {
                        isMainPlayerFight = false;
                        duelWinner = Duel(fighter1, fighter2);
                    }
                    
                    if (duelWinner == players[j]) roundLoserIndex.Add(players.Count() - 1 - j);
                    else roundLoserIndex.Add(j);
                }
                roundLoserIndex.Sort();
                roundLoserIndex.Reverse();
                //Remove players that lost from players list 
                foreach (int index in roundLoserIndex)
                {
                    players.RemoveAt(index);
                }
                //clear indexes for new cycle
                roundLoserIndex.Clear();
            }

            isMainPlayerFight = false;
            return players[0];
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

        private void Opponent_DamageReceived(object sender, DamageEventArgs e)
        {
             fightLogs += $"{e.Player.Name} has {e.Player.Health} HP after receiving {e.DamageTaken} damage.\n";
        }

        private void Player_DamageReceived(object sender, DamageEventArgs e)
        {
             fightLogs += $"{e.Player.Name} has {e.Player.Health} HP after receiving {e.DamageTaken} damage.\n";
        }
    }
}
