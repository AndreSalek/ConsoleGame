using GameLibrary.Items;
using System;
namespace GameLibrary.Player
{
    public abstract class PlayerModel : IPlayer
    {
        private int _health;
        private int _maxHealth;
        private int _vitality;

        public string Name { get; set; }
        public abstract string Class { get; set; }

        public abstract int Ranking { get; set; } 
        public Weapon EquippedWeapon { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public int Vitality
        {
            get { return _vitality; }
            set
            {
                _vitality = value;
                UpdateMaxHealth();
            }
        }
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

        public int GetHealth()
        {
            return _maxHealth;
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

        public int GetAttribute(int attributeNumber)
        {
            if (attributeNumber == 0) return this.Strength;
            else if (attributeNumber == 1) return this.Dexterity;
            else if (attributeNumber == 2) return this.Intelligence;
            else if (attributeNumber == 3) return this.Vitality;
            return 0;
        }
        public int ImproveAttribute(int attributeNumber, int addToAttribute)
        {
            if (attributeNumber == 0) this.Strength += addToAttribute;
            else if (attributeNumber == 1) this.Dexterity += addToAttribute;
            else if (attributeNumber == 2) this.Intelligence += addToAttribute;
            else if (attributeNumber == 3) this.Vitality += addToAttribute;
            UpdateDamage();
            return 0;
        }
    }
}
