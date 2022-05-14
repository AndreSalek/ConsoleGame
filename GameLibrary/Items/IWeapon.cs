using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Items
{
    interface IWeapon
    {
        int MinDamage { get; set; }
        int MaxDamage { get; set; }
    }
}
