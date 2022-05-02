using System;
using GameLibrary.Items;

namespace GameLibrary.Player
{
    public class Mage : PlayerModel
    {
        public override string Class { get; set; } = "Mage";
        public override int Ranking { get; set; }

        public Mage(string name, int str, int dex, int intell, int vit, int ranking)
        {
            this.Name = name;
            this.Strength = str;
            this.Dexterity = dex;
            this.Intelligence = intell;
            this.Vitality = vit;
            this.Ranking = ranking;     //last rank will be assigned at creating instance
            this.EquippedWeapon = new Weapon("Starter weapon", 10, 15);
            UpdateDamage();
            UpdateMaxHealth();
            RestoreHealth();
        }
        public override bool BlockOrDodge(PlayerModel attacker)
        {
            return false;
        }

        public override void ReceiveReward(int experience, int gold, IItem item)
        {
            throw new NotImplementedException();
        }
    }
}
