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
            return new FloorNotFontSprite(_game, _game.Window.ClientBounds.Width/2f, (_game.Window.ClientBounds.Height / 2f) + 100, 100, 5);
        }

        public  IFloor CreateFloorSprite1()
        {
            return new FloorNotFontSprite(_game, (_game.Window.ClientBounds.Width / 2f)-200, (_game.Window.ClientBounds.Height / 2f) + 100, 100, 5);
        }

        public IFloor CreateFloorSprite2()
        {
            return new FloorNotFontSprite(_game, (_game.Window.ClientBounds.Width / 2f) -200, (_game.Window.ClientBounds.Height / 2f)-20, 100, 5);
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
