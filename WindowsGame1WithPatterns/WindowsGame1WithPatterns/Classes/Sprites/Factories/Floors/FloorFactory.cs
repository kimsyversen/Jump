using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Concretes;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Floors
{
    class FloorFactory : FloorCreator
    {
        public override IFloor CreateFontSprite(Game game)
        {
            return new FloorSprite(game);
        }
    }
}
