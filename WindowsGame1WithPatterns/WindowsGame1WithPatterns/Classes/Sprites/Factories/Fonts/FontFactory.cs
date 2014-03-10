using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts.Concretes;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts
{
    class FontFactory : FontCreator
    {
        private Game _game;

        public FontFactory(Game game)
        {
            _game = game;
        }

        public override IFont PlayerScoreFont(IPlayer subject)
        {
            return new CoordinateFontSprite(_game, subject);
        }

        public override SimpleFont Font(String fontText, Color color, Vector2 position)
        {
            return new SimpleFont(_game, fontText, color, position);
        }
    }
}
