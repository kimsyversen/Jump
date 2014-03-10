using Microsoft.Xna.Framework;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Platform.Concretes;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Platform
{
    class PlatformFactory : PlatformCreator
    {
        private Game _game;

        public PlatformFactory(Game game)
        {
            _game = game;

        }
        public override IPlatform CreateFloorSprite()
        {
            return new PlatformNotFontSprite(_game, _game.Window.ClientBounds.Width/2f, (_game.Window.ClientBounds.Height / 2f) + 100, 100, 5);
        }

        public  IPlatform CreateFloorSprite1()
        {
            return new PlatformNotFontSprite(_game, (_game.Window.ClientBounds.Width / 2f)-200, (_game.Window.ClientBounds.Height / 2f) + 100, 100, 5);
        }

        public IPlatform CreateFloorSprite2()
        {
            return new PlatformNotFontSprite(_game, (_game.Window.ClientBounds.Width / 2f) -200, (_game.Window.ClientBounds.Height / 2f)-20, 100, 5);
        }

        public IPlatform CreateFloorSpriteInputs(float startX, float startY, int width, int height)
        {
            return new PlatformNotFontSprite(_game, startX, startY, width, height);
            
        }
    }
}
