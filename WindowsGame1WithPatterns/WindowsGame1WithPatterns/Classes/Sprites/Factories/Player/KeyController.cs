using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Player
{
    class KeyController
    {
        public Keys Left { get; set; }
        public Keys Right { get; set; }
        public Keys Jump { get; set; }
        public KeyController(Keys left, Keys right, Keys jump)
        {
            Left = left;
            Right = right;
            Jump = jump;
        }
    }
}
