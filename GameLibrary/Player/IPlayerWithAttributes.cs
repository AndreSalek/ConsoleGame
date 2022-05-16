
namespace GameLibrary.Player
{
    interface IPlayerWithAttributes : IPlayer, IAttribute
    {
        void ImproveAttribute(string attribute, int number);
    }
}
