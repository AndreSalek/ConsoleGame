using GameLibrary.Items;
using System;
namespace GameLibrary.Player
{
    public abstract class Player : IPlayerWithAttributes
    {
        private int _health;
        private int _maxHealth;
        public string Name { get; set; }
        public abstract string Class { get; set; }
        public Weapon EquippedWeapon { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public int Vitality { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int Health                     
        {
            get { return _health; }
            //it is capped on 0 so player can easily restore HP
            set  { _health = value; if (_health < 0) _health = 0; }
        }

        public event EventHandler<DamageEventArgs> DamageReceived;

        //Simulates 1v1 battle between two players
        //Battle is divided into turns
        //1 turn is when both players had a chance to attack
        public Player Duel(Player opponent)
        {
            Player winner;
            int turn = 0;
            var firstAttack = GenerateNumberInRange(0, 100);
            bool attackerFirst = (firstAttack <= 50) ? true : false; 
            while (Health > 0 && opponent.Health > 0)
            {
                ++turn;
                Console.WriteLine($"Turn { turn} started");
                //method SimulateTurn makes player that is passed first as parameter attack first
                if (attackerFirst) SimulateTurn(this, opponent);
                else SimulateTurn(opponent, this);
            }
            if (Health == 0) winner = opponent;
            else winner = this;
            RestoreHealth();
            opponent.RestoreHealth();

            return winner;
        }
        //simulates 1 turn fight between 2 players
        //1 turn is passed when both players attack
        public void SimulateTurn(Player player, Player player1)
        {
            //try to block attack, player attacks first
            bool mitigated = BlockOrDodge(player);      //attacker
            bool mitigated1 = BlockOrDodge(player1);    //deffender
            //I want to trigger DamageReceived event even when the damage is reduced to 0 so later I can output text to console or log after each attack
            if (mitigated) OnDamageReceived(player, new DamageEventArgs { Player = player1, DamageTaken = 0 });
            else
            {
                var dmg = GenerateNumberInRange(player.MinDamage, player.MaxDamage);
                player1.Health -= dmg;
                OnDamageReceived(player, new DamageEventArgs { Player = player1, DamageTaken = dmg });
            }
            if (player.Health == 0 || player1.Health == 0) return;
            if (mitigated1) OnDamageReceived(player1, new DamageEventArgs { Player = player, DamageTaken = 0 });
            else
            {
                var dmg = GenerateNumberInRange(player1.MinDamage, player1.MaxDamage);
                player.Health -= dmg;
                OnDamageReceived(player1, new DamageEventArgs { Player = player, DamageTaken = dmg });
            }
        }
        //this method decides how much damage will be dealt based on player attributes and min max damage
        public int GenerateNumberInRange(int minValue, int maxValue)
        {
            return new Random().Next(minValue, maxValue);
        }

        public abstract bool BlockOrDodge(Player attacker);
        protected virtual void OnDamageReceived(object sender, DamageEventArgs e)
        {
            DamageReceived?.Invoke(this, e);
        }
        //restore to max health
        public void RestoreHealth()
        {
            Health += _maxHealth - Health;
        }
        //return main attribute based on class, needed for damage formula
        public int GetMainAttributeValue()
        {
            if (Class == "Warrior") return Strength;
            else if (Class == "Scout") return Dexterity;
            else return Intelligence;
        }
        //class damage scaling
        public void UpdateDamage()
        {
            MinDamage = EquippedWeapon.MinDamage * (1 + GetMainAttributeValue() / 10);
            MaxDamage = EquippedWeapon.MaxDamage * (1 + GetMainAttributeValue() / 10);
            //not checking for warrior because he has MaxHealth * 2, so attack will not be multiplied
            if(Class == "Mage")
            {
                MinDamage *= 2;
                MaxDamage *= 2;
            }
            else if (Class == "Scout")
            { 
                MinDamage = Convert.ToInt32(Math.Floor(MinDamage * 1.5));
                MaxDamage = Convert.ToInt32(Math.Floor(MaxDamage * 1.5));
            }
        }
        //class max hp scaling and refresh
        public void UpdateMaxHealth()
        {
            _maxHealth = Vitality * 10 ;
            if (Class == "Warrior") _maxHealth *= 2;
            else if (Class == "Scout") _maxHealth = Convert.ToInt32(Math.Floor(_maxHealth * 1.5));
            else return;
        }
        public abstract void ReceiveReward();
    }
}
