using Microsoft.Xna.Framework;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Platform.Concretes;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Platform
{
    class PlatformFactory : PlatformCreator
    {
        private Game _game;

        public PlatformFactory(Game game)
        {
            _game = game;

        }
        public override IPlatform CreatePlatformSprite(IPlayer subject)
        {
            return new PlatformNotFontSprite(_game, _game.Window.ClientBounds.Width/2f, (_game.Window.ClientBounds.Height / 2f) + 100, 100, 5);
        }

        public IPlatform CreateFloorSprite1(IPlayer subject)
        {
            return new PlatformNotFontSprite(_game, (_game.Window.ClientBounds.Width / 2f)-200, (_game.Window.ClientBounds.Height / 2f) + 100, 100, 5);
        }

        public IPlatform CreateFloorSprite2(IPlayer subject)
        {
            return new PlatformNotFontSprite(_game, (_game.Window.ClientBounds.Width / 2f) -200, (_game.Window.ClientBounds.Height / 2f)-20, 100, 5);
        }

        public IPlatform CreateFloorSpriteInputs(float startX, float startY, int width, int height)
        {
            return new PlatformNotFontSprite(_game, startX, startY, width, height);
            
        }
        /*public override IPlatform CreateFloorSpriteTwo()
        {
            return new PlatformNotFontSprite(_game, 100, 20);
        }*/
    }
}
