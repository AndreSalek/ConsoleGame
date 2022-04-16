using System;

namespace GameLibrary.Player
{
    public interface IPlayer
    {
        public string Name { get; set; }
        public string Class { get; set; }
        //Vitality*10 = Health, is used for fight damage calculation
        public int Health { get; set; }
        //Min/Max damage * (1 + main attribute / 10)
        //class affects damage too
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public event EventHandler<DamageEventArgs> DamageReceived;

        Player Duel(Player opponent);
        void ReceiveReward();
        void RestoreHealth();
        public int GetMainAttributeValue();
        public void UpdateDamage();

    }
}
