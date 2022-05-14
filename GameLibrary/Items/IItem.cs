namespace GameLibrary
{
    interface IItem
    {
        string Name { get; set; }
        //Weapon,helmet,armor etc.
        string Type { get; set; }
    }
}