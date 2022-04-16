﻿
namespace GameLibrary.Player
{
    public interface IAttribute
    {
        //Warrior main attribute
        public int Strength { get; set; }
        //Scout main attribute
        public int Dexterity { get; set; }
        //Mage main attribute
        public int Intelligence { get; set; }
        //Vitality * 10 is Player's max health
        public int Vitality { get; set; }
    }
}
