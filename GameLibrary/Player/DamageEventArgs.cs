using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Player
{
    public class DamageEventArgs : EventArgs
    {
        public IPlayer Player { get; set; }
        public int DamageTaken { get; set; }
        
    }
}
