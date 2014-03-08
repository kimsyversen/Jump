using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame1WithPatterns.Classes.Sprites;

namespace WindowsGame1WithPatterns.Classes
{
    class SimpleFont : Sprite
    {
        private SpriteFont _font;
        private String _fontText;

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
    }
}
