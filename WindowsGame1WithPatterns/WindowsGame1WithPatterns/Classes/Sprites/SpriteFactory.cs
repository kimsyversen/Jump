using Microsoft.Xna.Framework;

namespace WindowsGame1WithPatterns.Classes.Sprites
{
    class SpriteFactory : SpriteCreator
    {
        public override Sprite CreatePlayerSprite(Game game)
        {
            return new PlayerSprite(game);
        }

        public override Sprite CreateFontSprite(Game game)
        {
           return new FontSprite(game);
        }
    }
}
