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
        event EventHandler<DamageEventArgs> DamageReceived;

        void ReceiveReward(int experience, int gold);
        void ReceiveDamage(PlayerModel attacker, int dmg);
        bool BlockOrDodge(PlayerModel attacker);
        void RestoreHealth();
        public int GetMainAttributeValue();
        public void UpdateDamage();

    }
}
