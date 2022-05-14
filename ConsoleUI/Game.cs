using System;
using System.Collections.Generic;
using System.IO;
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

        private Interaction interaction = new Interaction();
        private Factory factory;

        public bool OpenApplication { get; private set; }

        public Game(PlayerModel player, Factory factory)
        {
            this.player = player;
            this.factory = factory;
            OpenApplication = true;
        }

        //main game loop
        public void Run()
        {
            while (OpenApplication)
            {
                string message = $"Class: { player.Class } \n";
                message += $"Name: {player.Name} \n";
                message += $"Weapon damage: {player.EquippedWeapon.MinDamage} - {player.EquippedWeapon.MaxDamage} \n";
                message += $"Gold: {player.Gold} \n";
                message += $"Level: {player.Level} \n";
                message += $"Experience: {player.CurrentExperience}/{player.NextLevelExperience}\n";
                message += $"_______________________\n";
                string[] options = { "Go on quest", "Duel", "Tournament", "Improve attributes", "Exit" };
                Menu mainMenu = new Menu(options, message);
                int index = mainMenu.Run();

                switch (index)
                {
                    case 0:
                        foreach (var model in factory.playerModels)
                        {
                            Console.WriteLine(model.Class);
                        }
                        Console.ReadLine();
                        break;
                    case 1:
                        //Decide which opponents to show in Duels
                        List<PlayerModel> opponentList = GetOpponents(3);
                        //Taking string information about opponents
                        string[] opponentOptions = PlayerInfoToArray(opponentList);
                        Console.WriteLine(opponentList[0].Class);
                        //choose opponent
                        Menu opponentsMenu = new Menu(opponentOptions, "Choose opponent you want to duel:");
                        //get chosen opponent index
                        int opponentIndex = opponentsMenu.Run();
                        PlayerModel opponent = opponentList[opponentIndex];
                        //events for creating fight log
                        player.DamageReceived += Player_DamageReceived;
                        opponent.DamageReceived += Opponent_DamageReceived;

                        PlayerModel winner = interaction.Duel(player, opponent);

                        string outcome = "";
                        if (winner == player)
                        {
                            int experience = (25 * winner.Level) * (1 + winner.Level/5);
                            int gold = 10 * winner.Level;
                            winner.ReceiveReward(experience, gold);
                            outcome += $"You have won. \n This fight earned you {experience} experience and {gold} gold";
                        }
                        else outcome += "Enemy has won.";

                        outcome += " Do you want to see the fight log?";
                        string[] logOptions = new string[2] { "Yes", "No" };
                        Menu menu = new Menu(logOptions, outcome);
                        int seeLog = menu.Run();
                        if (seeLog == 0)
                        {
                            Menu logMenu = new Menu(new string[1] {"Close"}, interaction.lastFightLog);
                            logMenu.Run();
                        }

                        break;
                    case 2:
                        //Tournament
                        break;
                    case 3:
                        //Improve attributes
                        break;
                    case 4:
                        OpenApplication = false;
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;

                }
            }
            
        }

        private string[] PlayerInfoToArray(List<PlayerModel> opponentList)
        {
            string playerInfo = "";
            foreach (PlayerModel model in opponentList)
            {
                playerInfo += $"Class: { model.Class }  Damage: {model.MinDamage}-{model.MaxDamage} Health: {model.Health} Strength: {model.Strength} Dexterity: {model.Dexterity} Intelligence: {model.Intelligence}\n";   
            }
            return playerInfo.Split("\n");
        }

        //returns list of opponents closest to player's ranking
        private List<PlayerModel> GetOpponents(int number)
        {
            List<PlayerModel> opponentList = new List<PlayerModel>();
            if (player.Ranking == 1) opponentList.AddRange(GetLowerRanking(3));
            else if (player.Ranking == factory.lastRank) opponentList.AddRange(GetHigherRanking(3));
            else
            {
                for (int i = 0; i < number; i++)
                {
                    int decision = new Random().Next(0, 100);
                    if (decision > 50) opponentList.AddRange(GetHigherRanking(1));
                    else opponentList.AddRange(GetHigherRanking(1));
                }
            }
            return opponentList;
        }

        private IEnumerable<PlayerModel> GetHigherRanking(int number)
        {
            foreach(PlayerModel model in factory.playerModels)
            {
                if (player.Ranking > model.Ranking && model.Ranking >= player.Ranking - number) yield return model;
            }
        }

        private IEnumerable<PlayerModel> GetLowerRanking(int number)
        {
            foreach (PlayerModel model in factory.playerModels)
            {
                if (player.Ranking < model.Ranking && model.Ranking <= player.Ranking + number) yield return model;
            }
        }

        private void Opponent_DamageReceived(object sender, DamageEventArgs e)
        {
            interaction.lastFightLog += $"{e.Player.Name} has {e.Player.Health} HP after receiving {e.DamageTaken} damage.\n";
        }

        private void Player_DamageReceived(object sender, DamageEventArgs e)
        {
            interaction.lastFightLog += $"{e.Player.Name} has {e.Player.Health} HP after receiving {e.DamageTaken} damage.\n";
        }
        
    }
}
