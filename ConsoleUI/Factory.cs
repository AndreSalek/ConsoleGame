using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Player;

namespace ConsoleUI
{
    public class Factory
    {
        public int lastRank = 0;
        public List<PlayerModel> playerModels = new List<PlayerModel>();
        //creating classes with starting attributes and giving last ranking
        public Warrior CreateWarrior(string name)
        {
            lastRank++;
            Warrior warrior = new Warrior(name, 15, 10, 10, 12, lastRank);
            playerModels.Add(warrior);
            return warrior;
        }
        public Scout CreateScout(string name)
        {
            lastRank++;
            Scout scout = new Scout(name, 10, 15, 10, 12, lastRank);
            playerModels.Add(scout);

            return scout;
        }
        public Mage CreateMage(string name)
        {
            lastRank++;
            Mage mage = new Mage(name, 10, 10, 15, 12, lastRank);
            playerModels.Add(mage);
            return mage;
        }

        public void CreateOpponents(int number)
        {
            for(int i = 0; i < number; i++)
            {
                int classNumber = new Random().Next(1,4);       // generate number between 1 and 4 (3 is max number that can ben rolled)
                Console.WriteLine(classNumber);
                if(classNumber == 1) CreateWarrior("");                   //I'll create some random names for them later
                else if (classNumber == 2) CreateScout("");
                else if (classNumber == 3) CreateMage("");
            }
        }
    }
}
