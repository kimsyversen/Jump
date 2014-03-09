using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace WindowsGame1WithPatterns.Classes.Managers.Magnus
{
    abstract class StateManager : DrawableGameComponent
    {
        protected static List<StateManager> StateManagers = new List<StateManager>();
        
        protected StateManager(Game game) : base(game)
        {
            //All components is default off
            Visible = false;
            Enabled = false;
            //Add newly created managers to the list (GameManager, MenuManager etc)
            StateManagers.Add(this);
        }

        public abstract StateManager ChangeState();

        public void Enable(bool value)
        {
            Visible = value;
            Enabled = value;
        }
    }
}
