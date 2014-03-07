using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Platform
{
    abstract class PlatformCreator
    {
        public abstract IPlatform CreatePlatformSprite(IPlayer subject);
    }
}
