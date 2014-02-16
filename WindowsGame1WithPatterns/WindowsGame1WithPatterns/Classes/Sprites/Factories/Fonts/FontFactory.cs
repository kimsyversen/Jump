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
        public override IFont CreateFont(Game game)
        {
            return new FontSprite(game);
        }
    }
}
