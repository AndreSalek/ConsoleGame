using GameLibrary.Player;
using System.Collections.Generic;

namespace GameLibrary
{
    interface IInteractions
    {
        void Duel(IPlayer player, IPlayer player1);
        void Tournament(List<IPlayer> tournamentList);
    }
}