using System;
using GameLibrary.Items;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Player
{
    class Scout : Player
    {
        public override string Class { get; set; } = "Scout";
        public Scout(string name, int str, int dex, int intell, int vit)
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

        protected override bool BlockOrDodge(IPlayer attacker)
        {
            //mage attacks cannot be blocked, so if attacking player is mage -> damage is always received
            if (attacker.Class == "Mage") return false;
            else
            {
                //Scouts  have 50 percent chance to dodge damage
                Random random = new Random();
                var outcome = random.Next(0, 100);
                if (outcome <= 50) return true;
                else return false;
            }
        }

        public override void ReceiveReward()
        {
            throw new NotImplementedException();
        }


    }
}
