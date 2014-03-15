using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.Sprites
{
    /// <summary>
    /// Everything in this class is common for fonts and player sprites
    /// </summary>
    abstract class Sprite
    {
        protected Vector2 Origin; //Origin for rotation
        protected Vector2 Position; // Position of sprite
        protected Color Color;
        protected float Rotate;
        protected float Scale;
        protected SpriteEffects SpriteEffects;
    }
}
