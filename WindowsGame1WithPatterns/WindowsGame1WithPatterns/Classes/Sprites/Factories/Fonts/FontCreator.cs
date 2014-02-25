using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts
{
    //List the different type of font that we want to screate
    abstract class FontCreator
    {
        public abstract IFont PlayerScoreFont(IPlayer subject);
    }
}
