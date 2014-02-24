using WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Player
{
    //"Things" that only players can do
    interface IPlayer : ISprite
    {
        void Test();

        //A player may have observers (fonts that shall be updated, ex. score)
        void RegisterObserver(IFont observer);
        void RemoveObserver(IFont observer);
        void NotifyObservers();
    }
}
