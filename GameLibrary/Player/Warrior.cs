﻿using GameLibrary.Items;
using GameLibrary.Interactions;
using System;


namespace GameLibrary.Player
{
    public class Warrior : PlayerModel
    {

        public override string Class { get;  set; } = "Warrior";
        public override int Ranking { get; set; }

        public Warrior(string name, int str, int dex, int intell, int vit, int ranking)
        {
            this.Name = name;
            this.Strength = str;
            this.Dexterity = dex;
            this.Intelligence = intell;
            this.Vitality = vit;
            this.Ranking = ranking;
            this.EquippedWeapon = new Weapon("Starter weapon", 10, 15);
            UpdateDamage();
            UpdateMaxHealth();
            RestoreHealth();
        }

        //returns true if attack was blocked or dodged
        public override bool BlockOrDodge(PlayerModel attacker)
        {
            //mage attacks cannot be blocked, so if attacking player is mage -> damage is always received
            if (attacker.Class == "Mage") return false;
            else{
                //Warriors have 25 percent chance to block damage
                var outcome = new Random().Next(0, 100);
                if (outcome <= 25) return true;
                else return false;
            }
        }
        public override void ReceiveReward(int experience, int gold, IItem item)
        {
            throw new NotImplementedException();
        }
        
    }
}
