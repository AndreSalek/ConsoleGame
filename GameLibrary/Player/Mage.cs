using System;
using GameLibrary.Items;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Player
{
    class Mage : Player
    {
        public override string Class { get; set; } = "Mage";
        public Mage(string name, int str, int dex, int intell, int vit)
        {
            this.Name = name;
            this.Strength = str;
            this.Dexterity = dex;
            this.Intelligence = intell;
            this.Vitality = vit;
            this.EquippedWeapon = new Weapon("Starter weapon", 10, 15);
            UpdateDamage();
            UpdateMaxHealth();
            RestoreHealth();
        }
        public override bool BlockOrDodge(Player attacker)
        {
            return false;
        }


        public override void ReceiveReward()
        {
            throw new NotImplementedException();
        }

    }
}
