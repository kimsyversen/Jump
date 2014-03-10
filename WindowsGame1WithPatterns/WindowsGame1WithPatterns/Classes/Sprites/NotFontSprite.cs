using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.Sprites
{

    //TODO: Bad classname, fix
    class NotFontSprite : Sprite, ISprite
    {
        protected const int DefaultMillisecondsPerFrame = 16; //60 times per second
        protected int CollisionOffset; //pixels outside of texture to not collide with
        protected Point FrameCurrent; //Index (x,y) for current frame in spritesheet/sprite
        protected Point FrameSize; //Size for each individual frame in spritesheet/sprite
        protected int MillisecondsPerFrame; //Milliseconds between framechanges
        protected Texture2D Texture;
        protected Point SheetSize; // Number of columns/rows in sprite sheet
        protected Vector2 Velocity; // PlayerSpeed of sprite
        protected int TimeSinceLastFrame = 0; //Milliseconds since last frame was drawn
        protected const int MinX = 0;
        protected const int MinY = 0;
        private Random _random;

        public Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle
                    (
                    (int)Position.X + CollisionOffset,
                    (int)Position.Y + CollisionOffset,
                    FrameSize.X - (CollisionOffset * 2),
                    FrameSize.Y - (CollisionOffset * 2)
                    );
            }
        }

        protected NotFontSprite() {}
        protected NotFontSprite(Texture2D texture, Vector2 position, Point frameSize, Point frameCurrent,
                                   Point sheetSize,
                                   float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects, Vector2 velocity,
                                   int collisionOffset, int timeSinceLastFrame)
            : this(
                texture, position, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, velocity,
                collisionOffset, timeSinceLastFrame, DefaultMillisecondsPerFrame)
        {
            
        }

        protected NotFontSprite(Texture2D texture, Vector2 position, Point frameSize, Point frameCurrent,
                                   Point sheetSize,
                                   float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects, Vector2 velocity,
                                   int collisionOffset, int millisecondsPerFrame, int timeSinceLastFrame)
        {
            CollisionOffset = collisionOffset;
            FrameCurrent = frameCurrent;
            FrameSize = frameSize;
            MillisecondsPerFrame = millisecondsPerFrame;
            Position = position;
            SheetSize = sheetSize;
            Velocity = velocity;
            Texture = texture;
            TimeSinceLastFrame = timeSinceLastFrame;
            SpriteEffects = spriteEffects;
            Scale = scale;
            Rotate = rotate;
            Origin = origin;
        }

        /// <summary>
        /// Handles update of animation of sprites
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="clientBounds"></param>
        public void Update(GameTime gameTime, Rectangle clientBounds)
        {
            //Animation

            //Count time since last frame
            TimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            //If its not time to update frame, return
            if (TimeSinceLastFrame <= MillisecondsPerFrame) return;

            TimeSinceLastFrame -= MillisecondsPerFrame;

            //Logic for choosing frames from spritesheets
            ++FrameCurrent.X;
            if (FrameCurrent.X < SheetSize.X) return;

            FrameCurrent.X = 0;
            ++FrameCurrent.Y;
            if (FrameCurrent.Y >= SheetSize.Y)
                FrameCurrent.Y = 0;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Texture,
                Position,
                new Rectangle(FrameCurrent.X * FrameSize.X, FrameCurrent.Y * FrameSize.Y, FrameSize.X, FrameSize.Y),
                Color.White, Rotate, Origin,
                Scale, SpriteEffects, 0);
        }

        public float RandomFloat(double minimum = 6, double maximum = 25)
        {
            _random = new Random();
            return (float)(_random.NextDouble() * (maximum - minimum) + minimum);
        }  
    }
}
