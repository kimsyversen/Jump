using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.Sprites.Concretes
{
    class Font : Sprite
    {
        /// <summary>
        /// Text for the sprite
        /// </summary>
        public string FontText { get; set; }
        /// <summary>
        /// The sprite itself
        /// </summary>
        public SpriteFont FontSprite { get; set; }

        public Font(Game game, SpriteFont font2, String fontText, Color fontColor, Vector2 fontPosition)
            : base(game)
        {
            FontSprite = font2;
            FontText = fontText;
            color = fontColor;
            rotate = 0;
            origin = Vector2.Zero;
            scale = 1.0f;
            spriteEffects = SpriteEffects.None;
            position = fontPosition;
        }

        public Font(Game game, String fontText, Color fontColor, Vector2 fontPosition)
            : base(game)
        {
            FontSprite = game.Content.Load<SpriteFont>(@"Font\SimpleFont");
            FontText = fontText;
            color = fontColor;
            rotate = 0;
            origin = Vector2.Zero;
            scale = 1.0f;
            spriteEffects = SpriteEffects.None;
            position = fontPosition;
        }

        /// <summary>
        /// Draw string
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(FontSprite, FontText, Position, Color);
        }

    }
}
