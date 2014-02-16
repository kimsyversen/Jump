using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame1WithPatterns.Classes.Sprites.Factory;

namespace WindowsGame1WithPatterns.Classes.Sprites
{
    class FloorSprite : Sprite, ISprite
    {
        private Game _game;

        public FloorSprite(Game game)
        {
            _game = game;
        }

        public FloorSprite(Texture2D texture, Vector2 position, Point frameSize, Point frameCurrent, Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects, Vector2 speed, int collisionOffset, int timeSinceLastFrame) : base(texture, position, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, speed, collisionOffset, timeSinceLastFrame)
        {
        }

        public FloorSprite(Texture2D texture, Vector2 position, Point frameSize, Point frameCurrent, Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects, Vector2 speed, int collisionOffset, int millisecondsPerFrame, int timeSinceLastFrame) : base(texture, position, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, speed, collisionOffset, millisecondsPerFrame, timeSinceLastFrame)
        {
        }

        public void CreateSprite()
        {
            throw new NotImplementedException();
        }

        public override Vector2 Direction
        {
            get { throw new NotImplementedException(); }
        }
    }
}
