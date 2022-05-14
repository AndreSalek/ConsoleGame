using System;
using GameLibrary.Items;

namespace GameLibrary.Player
{
    public class Scout : PlayerModel
    {
        public override string Class { get; set; } = "Scout";
        public override int Ranking { get; set; }
        public Scout(string name, int str, int dex, int intell, int vit, int ranking, int Gold, int Level)
        {
            this.Name = name;
            this.Strength = str;
            this.Dexterity = dex;
            this.Intelligence = intell;
            this.Vitality = vit;
            this.Ranking = ranking;
            this.EquippedWeapon = new Weapon("Starter weapon", 10, 15);
            this.Gold = Gold;
            this.Level = Level;
            NextLevelExperienceUpdate();
            UpdateDamage();
            UpdateMaxHealth();
            RestoreHealth();
        }

        public override bool BlockOrDodge(PlayerModel attacker)
        {
            //mage attacks cannot be blocked, so if attacking player is mage -> damage is always received
            if (attacker.Class == "Mage") return false;
            else
            {
                //Scouts  have 50 percent chance to dodge damage
                var outcome = new Random().Next(0, 100);
                if (outcome <= 50) return true;
                else return false;
            }
        }
    }
}
