using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Floors
{
    interface IFloor : ISprite
    {
         Rectangle Collide { get; }
         Vector2 FloorPosition { get; set; }
         Texture2D FloorTexture { get; set; }

    }
}
