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
            return new PlatformNotFontSprite(_game, 0, (_game.Window.ClientBounds.Height) - 5, _game.Window.ClientBounds.Width, 5);
        }

        public IPlatform CreateFloorSpriteInputs(float startX, float startY, int width, int height)
        {
            return new PlatformNotFontSprite(_game, startX, startY, width, height);
            
        }
    }
}
