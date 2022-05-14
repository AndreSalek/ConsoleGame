
namespace GameLibrary.Player
{
    //I made this interface so it can potentialy be reused for items
     interface IAttribute
    {
        //Warrior main attribute
        int Strength { get; set; }
        //Scout main attribute
        int Dexterity { get; set; }
        //Mage main attribute
        int Intelligence { get; set; }
        //Vitality * 10 is Player's max health
        int Vitality { get; set; }
    }
}
