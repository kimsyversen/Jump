using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts.Concretes
{
    class SimpleFont : Sprite
    {
        private SpriteFont _font;
        private String _fontText;
        public SimpleFont() {}
        public SimpleFont(Game game, SpriteFont font, String fontText, Color color, Vector2 position)
        {
            _font = font;
            _fontText = fontText;
            Color = color;
            Rotate = 0;
            Origin = Vector2.Zero;
            Scale = 1.0f;
            SpriteEffects = SpriteEffects.None;
            Position = position;
        }

        public SimpleFont(Game game, String fontText, Color color, Vector2 position)
        {
            _font = game.Content.Load<SpriteFont>(@"Font\SimpleFont");
            _fontText = fontText;
            Color = color;
            Rotate = 0;
            Origin = new Vector2(_font.MeasureString(_fontText).X / 2f, _font.MeasureString(_fontText).Y / 2f);
            Scale = 1.0f;
            SpriteEffects = SpriteEffects.None;
            Position = position;
        }
        #region properties
        public Vector2 Origin2
        {
            get { return Origin; }
            set { Origin = value; }
        }

        public Vector2 Position2
        {
            get { return Position; }
            set { Position = value; }
        }

        public Color Color2
        {
            get { return Color; }
            set { Color = value; }
        }

        public float Rotate2
        {
            get { return Rotate; }
            set { Rotate = value; }
        }

        public float Scale2
        {
            get { return Scale; }
            set { Scale = value; }
        }

        public SpriteEffects SpriteEffects2
        {
            get { return SpriteEffects; }
            set { SpriteEffects = value; }
        }

        public string FontText
        {
            get { return _fontText; }
            set { _fontText = value; }
        }
        public SpriteFont Font
        {
            get { return _font; }
            set { _font = value; }
        }
        #endregion
    }
}
