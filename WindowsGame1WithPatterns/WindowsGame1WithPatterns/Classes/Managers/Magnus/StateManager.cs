using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Collections;

namespace WindowsGame1WithPatterns.Classes.Managers.Magnus
{
    abstract class StateManager : DrawableGameComponent
    {
        private static Dictionary<string, StateManager> StateManagers = new Dictionary<string, StateManager>();
        private string _managerId;
        private StateManager _changeState;

        public StateManager ChangeState
        {
            get
            {
                StateManager changingState = _changeState;
                _changeState = null;
                return changingState;
            }
            private set
            {
                _changeState = value; 
            }
        }

        protected StateManager(Game game, string managerId, string stateId) : base(game)
        {
            //All components is default off
            Visible = false;
            Enabled = false;
            //Add newly created managers to the list (GameManager, MenuManager etc)
            StateManagers.Add(string.Concat(managerId, stateId), this);

            _managerId = managerId;
        }

        public void Enable(bool value)
        {
            Visible = value;
            Enabled = value;
        }

        protected void ChangeStateTo(string stateId)
        {
            ChangeState = StateManagers[string.Concat(_managerId, stateId)];
        }
    }
}
