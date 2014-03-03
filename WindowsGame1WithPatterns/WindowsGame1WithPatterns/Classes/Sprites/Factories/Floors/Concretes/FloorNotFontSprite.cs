﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Floors.Concretes
{
    class FloorNotFontSprite : NotFontSprite, IFloor
    {
        private Game _game;


        public FloorNotFontSprite(Game game)
            : this(game.Content.Load<Texture2D>(@"stikker"),
                new Vector2(game.Window.ClientBounds.Width / 2f, (game.Window.ClientBounds.Height / 2f)+100), new Point(100, 20), new Point(0, 0),
                new Point(0, 0), 0f, Vector2.Zero, 1f, SpriteEffects.None, new Vector2(0, 0), 0, 100)
        {
            _game = game;
        }

        public FloorNotFontSprite(Texture2D texture, Vector2 spritePosition, Point frameSize, Point frameCurrent, Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects, Vector2 speed, int collisionOffset, int timeSinceLastFrame) : base(texture, spritePosition, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, speed, collisionOffset, timeSinceLastFrame)
        {
        }

        public FloorNotFontSprite(Texture2D texture, Vector2 spritePosition, Point frameSize, Point frameCurrent, Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects, Vector2 speed, int collisionOffset, int millisecondsPerFrame, int timeSinceLastFrame) : base(texture, spritePosition, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, speed, collisionOffset, millisecondsPerFrame, timeSinceLastFrame)
        {
        }

         public Rectangle Collide { get { return CollisionRectangle; } set; }

        public new void Update(GameTime gameTime, Rectangle clientBounds)
        {
            
        }

        public new void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
