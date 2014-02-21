using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Floors.Concretes;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Floors
{
    class FloorFactory : FloorCreator
    {
        private Game _game;

        public FloorFactory(Game game)
        {
            _game = game;

        }
        public override IFloor CreateFontSprite()
        {
            return new FloorSprite(_game);
        }
    }
}
