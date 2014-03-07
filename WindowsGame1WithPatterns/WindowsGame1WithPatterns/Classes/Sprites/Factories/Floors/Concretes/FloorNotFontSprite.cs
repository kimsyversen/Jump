using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Floors.Concretes
{
    class FloorNotFontSprite : NotFontSprite, IFloor
    {
        private Game _game;
        

        public FloorNotFontSprite(Game game, float x, float y, int width, int height)
            : this(game.Content.Load<Texture2D>(@"stikker"),
                new Vector2(x, y), new Point(width, height), new Point(0, 0),
                new Point(0, 0), 0f, Vector2.Zero, 1f, SpriteEffects.None, new Vector2(0, 0), 0, 100)
        {
            _game = game;
        }

        public FloorNotFontSprite(Texture2D texture, Vector2 position, Point frameSize, Point frameCurrent, Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects, Vector2 velocity, int collisionOffset, int timeSinceLastFrame) : base(texture, position, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, velocity, collisionOffset, timeSinceLastFrame)
        {
        }

        public FloorNotFontSprite(Texture2D texture, Vector2 position, Point frameSize, Point frameCurrent, Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects, Vector2 velocity, int collisionOffset, int millisecondsPerFrame, int timeSinceLastFrame) : base(texture, position, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, velocity, collisionOffset, millisecondsPerFrame, timeSinceLastFrame)
        {
        }

        public Rectangle Collide
        {
            get { return CollisionRectangle; }
           
        }

        public Vector2 FloorPosition
        {
            get { return Position; }
            set { Position = value; }
        }

        public Texture2D FloorTexture
        {
            get { return Texture; }
            set { Texture = value; }
        }

        public new void Update(GameTime gameTime, Rectangle clientBounds)
        {
            
        }

        public new void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override String ToString()
        {
            return "X Position: " + this.FloorPosition.X.ToString() + ", Y position: " + this.FloorPosition.Y.ToString();
        }
    }
}
