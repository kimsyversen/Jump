using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts.Concretes;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts
{
    class FontFactory : FontCreator
    {
        private Game _game;

        public FontFactory(Game game)
        {
            _game = game;
        }

        public override IFont CreateFont()
        {
            return new FontSprite(_game);
        }
    }
}
