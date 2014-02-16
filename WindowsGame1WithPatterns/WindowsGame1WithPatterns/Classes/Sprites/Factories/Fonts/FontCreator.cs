using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts
{
   abstract class FontCreator
    {
       public abstract IFont CreateFont(Game game);
    }
}
