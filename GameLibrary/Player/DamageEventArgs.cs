using System;

namespace GameLibrary.Player
{
    public class DamageEventArgs : EventArgs
    {
        public IPlayer Player { get; set; }
        public int DamageTaken { get; set; }
        
    }
}
