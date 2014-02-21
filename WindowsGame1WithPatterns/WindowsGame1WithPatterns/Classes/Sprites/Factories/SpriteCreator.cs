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
    abstract class SpriteCreator
    {
        public abstract FontFactory CreateFontFactory();
        public abstract PlayerFactory CreatePlayerFactory();
        public abstract FloorFactory CreateFloorFactory();
    }
}
