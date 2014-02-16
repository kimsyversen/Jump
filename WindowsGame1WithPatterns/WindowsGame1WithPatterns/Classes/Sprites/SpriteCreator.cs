using Microsoft.Xna.Framework;

namespace WindowsGame1WithPatterns.Classes.Sprites
{
    abstract class SpriteCreator
    {
        public abstract Sprite CreatePlayerSprite(Game game);
        public abstract Sprite CreateFontSprite(Game game);
    }
}
