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
        //creating classes with starting attributes
        public Warrior CreateWarrior(string name)
        {
            return new Warrior(name, 15, 10, 10, 12);
        }
        public Scout CreateScout(string name)
        {
            return new Scout(name, 10, 15, 10, 12);
        }
        public Mage CreateMage(string name)
        {
            return new Mage(name, 10, 10, 15, 12);
        }
    }
}
