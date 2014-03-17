using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.Sprites.Concretes
{
    class Font : Sprite
    {
        private SpriteFont _font;
        private String _fontText;
        
        public Font(Game game, SpriteFont font2, String fontText, Color color, Vector2 position) : base(game)
        {
            _font = font2;
            _fontText = fontText;
            this.color = color;
            rotate = 0;
            origin = Vector2.Zero;
            scale = 1.0f;
            spriteEffects = SpriteEffects.None;
            base.position = position;
        }

        public Font(Game game, String fontText, Color color, Vector2 position)
            : base(game)
        {
            _font = game.Content.Load<SpriteFont>(@"Font\SimpleFont");
            _fontText = fontText;
            base.color = color;
            rotate = 0;
            origin = Vector2.Zero;
            scale = 1.0f;
            spriteEffects = SpriteEffects.None;
            base.position = position;
        }

        public string FontText
        {
            get { return _fontText; }
            set { _fontText = value; }
        }

        public SpriteFont FontSprite
        {
            get { return _font; }
            set { _font = value; }
        }



        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, _fontText, Position, Color);
        }
    }
}
