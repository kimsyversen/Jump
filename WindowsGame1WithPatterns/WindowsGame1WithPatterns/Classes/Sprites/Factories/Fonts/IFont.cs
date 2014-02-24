using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts
{
    //"Things fonts can do"
    interface IFont : ISprite
    {
        void UpdateScore(int score);
    }
}
