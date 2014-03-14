using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1WithPatterns
{
    class StateMan
    {
        public enum States
        {
            InGame,
            Multiplayer,
            Singleplayer
        }

        public bool isMultiplayer;
    }
}
