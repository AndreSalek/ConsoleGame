using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Items
{
    public class Weapon : IItemWeapon
    {
        public string Name { get; set; }
        public string Type { get; set; } = "weapon";
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }

        public Weapon(string name, int minDamage, int maxDamage)
        {
            this.Name = name;
            this.MinDamage = minDamage;
            this.MaxDamage = maxDamage;
        }
    }
}
