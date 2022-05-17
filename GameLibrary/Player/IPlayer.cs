using System;

namespace GameLibrary.Player
{
     public interface IPlayer
    {
        string Name { get; set; }
        string Class { get; set; }
        int Ranking { get; set; }
        //Vitality*10 = Health, is used for fight damage calculation
        int Health { get; set; }
        //Min/Max damage * (1 + main attribute / 10)
        //class affects damage too
        int MinDamage { get; set; }
        int MaxDamage { get; set; }
        int Gold { get; set; }
        int Level { get; set; }          
        //Warrior main attribute
        int Strength { get; set; }
        //Scout main attribute
        int Dexterity { get; set; }
        //Mage main attribute
        int Intelligence { get; set; }
        //Vitality * 10 is Player's max health
        int Vitality { get; set; }
        int CurrentExperience { get; set; }
        int NextLevelExperience { get; set; }
        event EventHandler<DamageEventArgs> DamageReceived;

        void ReceiveReward(int experience, int gold);
        void ReceiveDamage(PlayerModel attacker, int dmg);
        bool BlockOrDodge(PlayerModel attacker);
        void RestoreHealth();
        public int GetMainAttributeValue();
        public void UpdateDamage();
    }
}
