using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsGame1WithPatterns.Classes.Managers.Magnus
{
    class GameManager : StateManager
    {
        public GameManager(Game game) : base(game)
        {

        }

        public override StateManager ChangeState()
        {
            throw new NotImplementedException();
        }
    }
}
