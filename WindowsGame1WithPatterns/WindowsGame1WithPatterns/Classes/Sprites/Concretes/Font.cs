using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.Sprites.Concretes
{
    class Font : Sprite
    {
        private SpriteFont _font2;
        private String _fontText;
        public Font() {}
        public Font(Game game, SpriteFont font2, String fontText, Color color, Vector2 position)
        {
            _font2 = font2;
            _fontText = fontText;
            base.color = color;
            rotate = 0;
            origin = Vector2.Zero;
            scale = 1.0f;
            spriteEffects = SpriteEffects.None;
            base.position = position;
        }

        public Font(Game game, String fontText, Color color, Vector2 position)
        {
            _font2 = game.Content.Load<SpriteFont>(@"Font2\Font2");
            _fontText = fontText;
            base.color = color;
            rotate = 0;
            origin = new Vector2(_font2.MeasureString(_fontText).X / 2f, _font2.MeasureString(_fontText).Y / 2f);
            scale = 1.0f;
            spriteEffects = SpriteEffects.None;
            base.position = position;
        }

 
      

        public string FontText
        {
            get { return _fontText; }
            set { _fontText = value; }
        }
        //TODO: Fix name?
        public SpriteFont Font2
        {
            get { return _font2; }
            set { _font2 = value; }
        }
       
    }
}
