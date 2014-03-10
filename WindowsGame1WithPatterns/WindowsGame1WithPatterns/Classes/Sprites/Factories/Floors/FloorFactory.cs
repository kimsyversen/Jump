using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Floors.Concretes;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Floors
{
    class FloorFactory : FloorCreator
    {
        private Game _game;

        public FloorFactory(Game game)
        {
            _game = game;

        }
        public override IFloor CreateFloorSprite()
        {
            return new FloorNotFontSprite(_game, 0, (_game.Window.ClientBounds.Height)-5, _game.Window.ClientBounds.Width, 5);
        }

        public IFloor CreateFloorSpriteInputs(float startX, float startY, int width, int height)
        {
            return new FloorNotFontSprite(_game, startX, startY, width, height);
            
        }
        /*public override IFloor CreateFloorSpriteTwo()
        {
            return new FloorNotFontSprite(_game, 100, 20);
        }*/
    }
}
