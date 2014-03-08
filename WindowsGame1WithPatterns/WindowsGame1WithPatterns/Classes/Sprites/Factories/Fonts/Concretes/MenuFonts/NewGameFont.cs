using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts.Concretes.MenuFonts
{
    class NewGameFont : SimpleFont
    {
        private Game _game;
        public NewGameFont(Game game, SpriteFont font, string fontText, Color color, Vector2 position) : base(game, font, fontText, color, position)
        {
            
        }
        public NewGameFont(Game game)
            : this(game, game.Content.Load<SpriteFont>("Menu/NewGame"), "New game", Color.Black, new Vector2(game.Window.ClientBounds.Width / 2f, game.Window.ClientBounds.Height / 3f))
        {
        }    
    }
}
