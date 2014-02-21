using WindowsGame1WithPatterns.Classes.Sprites.Factories.Floors;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories
{
    abstract class SpriteCreator
    {
        public abstract FontFactory CreateFontFactory();
        public abstract PlayerFactory CreatePlayerFactory();
        public abstract FloorFactory CreateFloorFactory();
    }
}
