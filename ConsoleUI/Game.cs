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

        private Interaction interaction;
        private Factory factory;
        string fightOutcome = "";

        public bool OpenApplication { get; private set; }

        public Game(Factory factory, PlayerModel player)
        {
            this.factory = factory;
            this.player = player;
            //factory.CreateCustomPlayer("me", 1, 50, 51, 52, 53);
            this.interaction = new Interaction(player);
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
                message += $"Player Damage: {player.MinDamage} - {player.MaxDamage}\n";
                message += $"Strength: {player.Strength}\n";
                message += $"Dexterity: {player.Dexterity}\n";
                message += $"Intelligence: {player.Intelligence}\n";
                message += $"Vitality: {player.Vitality} (Health: {player.GetHealth()})\n";
                message += $"Gold: {player.Gold} \n";
                message += $"Level: {player.Level} \n";
                message += $"Experience: {player.CurrentExperience}/{player.NextLevelExperience}\n";
                message += $"Ranking: {player.Ranking} \n";
                message += $"_______________________\n";
                string[] options = { "Duel", "Tournament", "Improve attributes", "Exit" };
                Menu mainMenu = new Menu(options, message);
                int index = mainMenu.Run();

                switch (index)
                {
                    case 0:
                        //Decide which opponents to show in Duels
                        List<PlayerModel> opponentList = GetOpponents(3);
                        //Taking string information about opponents
                        string[] opponentOptions = PlayerInfoToArray("","",opponentList);
                        //choose opponent
                        Menu opponentsMenu = new Menu(opponentOptions, "Choose opponent you want to duel:");
                        //get chosen opponent index
                        int opponentIndex = opponentsMenu.Run();
                        PlayerModel opponent = opponentList[opponentIndex];

                        PlayerModel winner = interaction.Duel(player, opponent);

                        
                        if (winner == player)
                        {
                            int experience = (25 * winner.Level) * (1 + winner.Level / 5);
                            int gold = 10 * winner.Level;
                            winner.ReceiveReward(experience, gold);
                            fightOutcome += $"You have won. \n This fight earned you {experience} experience and {gold} gold";
                        }
                        else fightOutcome += "Enemy has won.";
                        DisplayLogsMenu();
                        break;
                    case 1:
                        //Tournament
                        List<PlayerModel> models = GetOpponents(7);                                                                      //Creating tournament list with opponents
                        string[] fightString = new string[2] { "Yes", "No" };
                        Menu tournamentMenu = new Menu(fightString, string.Join("\n",PlayerInfoToArray("Players in Tournament:\n", "Are you ready to fight?", models)));
                        int playerDecision = tournamentMenu.Run();
                        if (playerDecision == 0)
                        {
                            models.Add(player);                                                                                             //adding player to tournament list
                            PlayerModel tournamentWinner = interaction.Tournament(models);                                                  //Startig tournament
                            int experience = (25 * player.Level) * (1 + player.Level);
                            int gold = 150 * player.Level;
                            if (tournamentWinner == player)
                            {
                                player.ReceiveReward(experience, gold);
                                player.Ranking--;
                                fightOutcome += $"You have won. \n This tournament earned you {experience} experience and {gold} gold and earned -1 ranking \n";
                            }
                            else
                            {
                                fightOutcome += "You have lost. Try again next time. \n";
                            }
                            DisplayLogsMenu();
                        }
                        break;
                    case 2:
                        //Improve attributes
                        string[] opt = new string[4] { "Strength", "Dexterity", "Intelligence", "Vitality" };
                        Menu attributesMenu = new Menu(opt, "Which attribute you want to improve ?");
                        int attributeNumber = attributesMenu.Run();
                        string[] numberOpt = new string[3] { $"+1 (Price: {GetPrice(attributeNumber, 1)})", $"+5 (Price: {GetPrice(attributeNumber, 5)})", $"+10 (Price: {GetPrice(attributeNumber, 10)})" };
                        Menu numberMenu = new Menu(numberOpt, "By how much you want to improve ?");
                        int improveNumber = numberMenu.Run();
                        int addToAttribute;
                        if (improveNumber == 0)
                        {
                            addToAttribute = 1;
                        }
                        else if (improveNumber == 1)
                        {
                            addToAttribute = 5;
                        }
                        else addToAttribute = 10;
                        int price = GetPrice(attributeNumber, addToAttribute);
                        bool success = ChargePlayerGoldAndImprove(attributeNumber, addToAttribute, price);
                        if (!success)
                        {
                            Console.WriteLine("You don't have enough gold.");
                            Console.ReadLine();
                        }
                        break;
                    case 3:
                        OpenApplication = false;
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;

                }
            }
            
        }

        private int GetPrice(int attributeNumber, int addToAttribute)
        {
            int price = 0;
            int attributeSubtotal;
            int attribute = player.GetAttribute(attributeNumber);
            for (int i = 0; i < addToAttribute; i++)
            {
                attributeSubtotal = attribute + i;
                price += (attributeSubtotal / 10) * attributeSubtotal;
            }
            return price;
        }
        private bool ChargePlayerGoldAndImprove(int attributeNumber,int addToAttribute, int price)
        {
            if (player.Gold > price)
            {
                player.Gold -= price;
                player.ImproveAttribute(attributeNumber, addToAttribute);
                return true;
            }
            else return false;
        }


        //fight log handling menu
        private void DisplayLogsMenu()
        {
            fightOutcome += " Do you want to see the fight log?\n";
            string[] logOptions = new string[2] { "Yes", "No" };
            Menu menu = new Menu(logOptions, fightOutcome);
            int seeLog = menu.Run();
            if (seeLog == 0)
            {
                Menu logMenu = new Menu(new string[1] { "Close" }, interaction.fightLogs);
                logMenu.Run();
            }
            interaction.fightLogs = interaction.fightLogs.Remove(0);
            fightOutcome = fightOutcome.Remove(0);
        }

        private string[] PlayerInfoToArray(string startMessage, string endMessage, List<PlayerModel> opponentList)
        {
            string playerInfo = startMessage;
            foreach (PlayerModel model in opponentList)
            {
                playerInfo += $"Class: { model.Class }  Damage: {model.MinDamage}-{model.MaxDamage} Health: {model.Health} Strength: {model.Strength} Dexterity: {model.Dexterity} Intelligence: {model.Intelligence}\n";   
            }
            playerInfo += endMessage;
            return playerInfo.Split("\n");
        }

        //returns list of opponents closest to player's ranking
        private List<PlayerModel> GetOpponents(int number)
        {
            List<PlayerModel> opponentList = new List<PlayerModel>();
            if (player.Ranking == 1) opponentList.AddRange(GetLowerRanking(number));
            else if (player.Ranking == factory.lastRank) opponentList.AddRange(GetHigherRanking(number));
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
    }
}
