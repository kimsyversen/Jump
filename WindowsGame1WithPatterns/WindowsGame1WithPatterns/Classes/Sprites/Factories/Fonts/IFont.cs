using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts
{
    //"Things fonts can do"
    interface IFont : ISprite
    {
        //Observer methods 
        void UpdateCoordinates(Vector2 coordinates);
    }
}
