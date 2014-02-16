using Microsoft.Xna.Framework;

namespace WindowsGame1WithPatterns.Classes.Sprites
{
    abstract class SpriteCreator
    {
        public abstract ISprite CreatePlayerSprite(Game game);
        public abstract ISprite CreateFontSprite(Game game);
        public abstract ISprite CreateFloorSprite(Game game);
    }
}
