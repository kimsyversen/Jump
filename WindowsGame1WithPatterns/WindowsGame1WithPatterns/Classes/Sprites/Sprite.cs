using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.Sprites
{
    abstract class Sprite
    {
        protected Vector2 Origin; //Origin for rotation
        protected Vector2 Position; // Position of sprite
        protected Color Color;
        protected float Rotate;
        protected float Scale;
        protected SpriteEffects SpriteEffects;

        public Vector2 Origin1
        {
            get { return Origin; }
            set { Origin = value; }
        }

        public Vector2 Position1
        {
            get { return Position; }
            set { Position = value; }
        }

        public Color Color1
        {
            get { return Color; }
            set { Color = value; }
        }

        public float Rotate1
        {
            get { return Rotate; }
            set { Rotate = value; }
        }

        public float Scale1
        {
            get { return Scale; }
            set { Scale = value; }
        }

        public SpriteEffects SpriteEffects1
        {
            get { return SpriteEffects; }
            set { SpriteEffects = value; }
        }
    }
}
