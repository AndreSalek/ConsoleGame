using GameLibrary.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Player
{
    public class Warrior : Player
    {

        public override string Class { get;  set; } = "Warrior";

        public Warrior(string name, int str, int dex, int intell, int vit)
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

        //returns true if attack was blocked or dodged
        public override bool BlockOrDodge(Player attacker)
        {
            //mage attacks cannot be blocked, so if attacking player is mage -> damage is always received
            if (attacker.Class == "Mage") return false;
            else{
                //Warriors have 25 percent chance to block damage
                var outcome = GenerateNumberInRange(0, 100);
                if (outcome <= 25) return true;
                else return false;
            }
        }
        public override void ReceiveReward()
        {
            throw new NotImplementedException();
        }
        
    }
}
