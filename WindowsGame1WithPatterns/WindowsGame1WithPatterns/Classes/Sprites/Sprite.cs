﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.Sprites
{

    //TODO: Bad classname, fix
    abstract class Sprite
    {

        protected Vector2 origin; //origin for rotation
        protected Vector2 position; // position of sprite
        protected Color color;
        protected float rotate;
        protected float scale;
        protected SpriteEffects spriteEffects;
        protected int collisionOffset; //pixels outside of texture to not collide with
        protected Point frameCurrent; //Index (x,y) for current frame in spritesheet/sprite
        protected Point frameSize; //Size for each individual frame in spritesheet/sprite
        protected int MillisecondsPerFrame; //Milliseconds between framechanges
        protected Texture2D texture;
        protected Point sheetSize; // Number of columns/rows in sprite sheet
        protected Vector2 velocity; // PlayerSpeed of sprite
        protected int TimeSinceLastFrame = 0; //Milliseconds since last frame was drawn
        protected const int MinX = 0;
        protected const int MinY = 0;
        protected const int DefaultMillisecondsPerFrame = 16; //60 times per second
        #region Properties

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public float Rotate
        {
            get { return rotate; }
            set { rotate = value; }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public SpriteEffects SpriteEffects
        {
            get { return spriteEffects; }
            set { spriteEffects = value; }
        }

        public int CollisionOffset
        {
            get { return collisionOffset; }
            set { collisionOffset = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        #endregion

        public Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle
                    (
                    (int)position.X + collisionOffset,
                    (int)position.Y + collisionOffset,
                    frameSize.X - (collisionOffset * 2),
                    frameSize.Y - (collisionOffset * 2)
                    );
            }
        }

        protected Sprite() {}
        protected Sprite(Texture2D texture, Vector2 position, Point frameSize, Point frameCurrent,
                                   Point sheetSize,
                                   float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects, Vector2 velocity,
                                   int collisionOffset, int timeSinceLastFrame)
            : this(
                texture, position, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, velocity,
                collisionOffset, timeSinceLastFrame, DefaultMillisecondsPerFrame)
        {
            
        }

        protected Sprite(Texture2D texture, Vector2 position, Point frameSize, Point frameCurrent,
                                   Point sheetSize,
                                   float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects, Vector2 velocity,
                                   int collisionOffset, int millisecondsPerFrame, int timeSinceLastFrame)
        {
            this.collisionOffset = collisionOffset;
            this.frameCurrent = frameCurrent;
            this.frameSize = frameSize;
            MillisecondsPerFrame = millisecondsPerFrame;
            this.position = position;
            this.sheetSize = sheetSize;
            this.velocity = velocity;
            this.texture = texture;
            TimeSinceLastFrame = timeSinceLastFrame;
            this.spriteEffects = spriteEffects;
            this.scale = scale;
            this.rotate = rotate;
            this.origin = origin;
        }

        /// <summary>
        /// Handles update of animation of sprites
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="clientBounds"></param>
        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            //Animation

            //Count time since last frame
            TimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            //If its not time to update frame, return
            if (TimeSinceLastFrame <= MillisecondsPerFrame) return;

            TimeSinceLastFrame -= MillisecondsPerFrame;

            //Logic for choosing frames from spritesheets
            ++frameCurrent.X;
            if (frameCurrent.X < sheetSize.X) return;

            frameCurrent.X = 0;
            ++frameCurrent.Y;
            if (frameCurrent.Y >= sheetSize.Y)
                frameCurrent.Y = 0;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture,
                position,
                new Rectangle(frameCurrent.X * frameSize.X, frameCurrent.Y * frameSize.Y, frameSize.X, frameSize.Y),
                Color.White, rotate, origin,
                scale, spriteEffects, 0);
        }
    }
}
