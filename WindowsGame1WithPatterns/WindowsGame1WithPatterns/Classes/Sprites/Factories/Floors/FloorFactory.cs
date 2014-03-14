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
            return new FloorNotFontSprite(_game, _game.Window.ClientBounds.Width/2f, (_game.Window.ClientBounds.Height) - 100, 100, 5);
        }

        public  IFloor CreateFloorSprite1()
        {
            return new FloorNotFontSprite(_game, (_game.Window.ClientBounds.Width / 2f)-200, (_game.Window.ClientBounds.Height) - 200, 100, 5);
        }

        public IFloor CreateFloorSprite2()
        {
            return new FloorNotFontSprite(_game, (_game.Window.ClientBounds.Width / 2f) -200, (_game.Window.ClientBounds.Height)-300, 100, 5);
        }

        public IFloor CreateFloorSprite3()
        {
            return new FloorNotFontSprite(_game, (_game.Window.ClientBounds.Width/2f), (_game.Window.ClientBounds.Height) - 400, 100, 5);
        }
        public IFloor CreateFloorSprite4()
        {
            return new FloorNotFontSprite(_game, (_game.Window.ClientBounds.Width / 2f) - 200, (_game.Window.ClientBounds.Height) - 500, 100, 5);
        }

        public IFloor CreateFloorSprite5()
        {
            return new FloorNotFontSprite(_game, (_game.Window.ClientBounds.Width / 2f), (_game.Window.ClientBounds.Height) - 600, 100, 5);
        }
        public IFloor CreateFloorSprite6()
        {
            return new FloorNotFontSprite(_game, (_game.Window.ClientBounds.Width / 2f) - 200, (_game.Window.ClientBounds.Height) - 700, 100, 5);
        }

        public IFloor CreateFloorSprite7()
        {
            return new FloorNotFontSprite(_game, (_game.Window.ClientBounds.Width / 2f), (_game.Window.ClientBounds.Height) - 800, 100, 5);
        }
        public IFloor CreateFloorSprite8()
        {
            return new FloorNotFontSprite(_game, (_game.Window.ClientBounds.Width / 2f) - 200, (_game.Window.ClientBounds.Height) - 900, 100, 5);
        }

        public IFloor CreateFloorSprite9()
        {
            return new FloorNotFontSprite(_game, (_game.Window.ClientBounds.Width / 2f), (_game.Window.ClientBounds.Height) - 1000, 100, 5);
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
