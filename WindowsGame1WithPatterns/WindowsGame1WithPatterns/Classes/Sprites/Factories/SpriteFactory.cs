using Microsoft.Xna.Framework;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Platform;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories
{
    class SpriteFactory : SpriteCreator
    {
        private Game _game;

        public SpriteFactory(Game game)
        {
            _game = game;
        }

        public override FontFactory CreateFontFactory()
        {
            return new FontFactory(_game);
        }

        public override PlayerFactory CreatePlayerFactory()
        {
            return new PlayerFactory(_game);
        }

        public override PlatformFactory CreatePlatformFactory()
        {
            return new PlatformFactory(_game);
        }
    }
}
