using GameLibrary.Items;
using System;
namespace GameLibrary.Player
{
    public abstract class PlayerModel : IPlayerWithAttributes
    {
        private int _health;
        private int _maxHealth;
        private int _level;
        private int _currentLevelExperience;

        public string Name { get; set; }
        public abstract string Class { get; set; }

        public abstract int Ranking { get; set; } 
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

        public int Gold { get; set; }
        public int Level { get; set; }          //set changes current experience to 0, updates next level experience
        public int CurrentExperience { get; set; }
        public int NextLevelExperience { get; set; }
        
        public event EventHandler<DamageEventArgs> DamageReceived;

        public abstract bool BlockOrDodge(PlayerModel attacker);
        protected virtual void OnDamageReceived(object sender, DamageEventArgs e)
        {
            DamageReceived?.Invoke(this, e);
        }
        protected void NextLevelExperienceUpdate()
        {
            if (CurrentExperience >= NextLevelExperience)    
            {
                CurrentExperience = 0;
                Level += 1;
                NextLevelExperience = 50 * Level * (1 + Level / 20);
            }
        }

        public void ReceiveDamage(PlayerModel attacker, int dmg)
        {
            bool mitigated = BlockOrDodge(attacker);
            if(mitigated) OnDamageReceived(attacker, new DamageEventArgs { Player = this, DamageTaken = 0 });
            else
            {
                Health -= dmg;
                OnDamageReceived(attacker, new DamageEventArgs { Player = this, DamageTaken = dmg });
            }
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
        public void ReceiveReward(int experience, int gold)
        {
            CurrentExperience += experience;
            Gold += gold;
            NextLevelExperienceUpdate();
        }


        public void ImproveAttribute(string attribute, int number)
        {
            if (attribute == "Vitality") this.Vitality += number;
        }
    }
}
