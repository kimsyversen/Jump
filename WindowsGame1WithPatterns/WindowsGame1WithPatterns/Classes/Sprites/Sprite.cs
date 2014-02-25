using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.Sprites
{
    abstract class Sprite
    {
        protected Vector2 Origin; //Origin for rotation
        protected Vector2 SpritePosition; // SpritePosition of sprite
        protected Color Color;
        protected float Rotate;
        protected float Scale;
        protected SpriteEffects SpriteEffects;
    }
}
