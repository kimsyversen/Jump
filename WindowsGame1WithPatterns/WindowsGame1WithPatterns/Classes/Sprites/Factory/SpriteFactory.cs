using Microsoft.Xna.Framework;

namespace WindowsGame1WithPatterns.Classes.Sprites
{
    class SpriteFactory : SpriteCreator
    {
        public override ISprite CreatePlayerSprite(Game game)
        {
            return new PlayerSprite(game);
        }

        public override ISprite CreateFontSprite(Game game)
        {
           return new FontSprite(game);
        }

        public override ISprite CreateFloorSprite(Game game)
        {
            return new FloorSprite(game);
        }
    }
}
