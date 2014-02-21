using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Floors;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player;

namespace WindowsGame1WithPatterns.Classes.Sprites
{
    class SpriteFactory : SpriteCreator
    {
        private Game _game;

        public SpriteFactory(Game game)
        {
            _game = game;

        }
        public override FontFactory CreateFontFactory()
        {
            return new FontFactory(_game);
        }

        public override PlayerFactory CreatePlayerFactory()
        {
            return new PlayerFactory(_game);
        }

        public override FloorFactory CreateFloorFactory()
        {
            return new FloorFactory(_game);
        }
    }
}
