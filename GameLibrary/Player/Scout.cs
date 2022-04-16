﻿using System;
using GameLibrary.Items;

namespace GameLibrary.Player
{
    public class Scout : Player
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

        public override bool BlockOrDodge(Player attacker)
        {
            //mage attacks cannot be blocked, so if attacking player is mage -> damage is always received
            if (attacker.Class == "Mage") return false;
            else
            {
                //Scouts  have 50 percent chance to dodge damage
                var outcome = GenerateNumberInRange(0, 100);
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
