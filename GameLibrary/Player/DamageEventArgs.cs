using System;

namespace GameLibrary.Player
{
    public class DamageEventArgs : EventArgs
    {
        public IPlayer Player { get; set; }
        public int DamageTaken { get; set; }
        public bool isMainPlayerFight { get; set; }             //serves for deciding which fight logs will be saved, so they can be displayed properly

    }
}
