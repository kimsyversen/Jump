using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Floors
{
    interface IFloor : ISprite
    {
         Rectangle Collide { get; }
    }
}
