using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Items
{
    public interface IWeapon
    {
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
    }
}
