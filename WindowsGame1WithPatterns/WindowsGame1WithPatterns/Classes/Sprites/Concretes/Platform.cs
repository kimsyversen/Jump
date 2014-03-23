using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.Sprites.Concretes
{
    class Platform : Sprite
    {
        public Platform(Game game, float x, float y, int width, int height)
            : this(game, game.Content.Load<Texture2D>(@"Figure\stikker"),
                new Vector2(x, y), new Point(width, height), new Point(0, 0),
                new Point(0, 0), 0f, Vector2.Zero, 1f, SpriteEffects.None, new Vector2(0, 0), 0, 100)
        {
        }

        public Platform(Game game, Texture2D texture, Vector2 position, Point frameSize, Point frameCurrent, 
            Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects, Vector2 velocity, int collisionOffset, int timeSinceLastFrame) : 
            base(game, texture, position, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, velocity, collisionOffset, timeSinceLastFrame)
        {
        }

        public Platform(Game game, Texture2D texture, Vector2 position, Point frameSize, Point frameCurrent, Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects, Vector2 velocity, int collisionOffset, int millisecondsPerFrame, int timeSinceLastFrame) : base(game, texture, position, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, velocity, collisionOffset, millisecondsPerFrame, timeSinceLastFrame)
        {
        }
    }
}
