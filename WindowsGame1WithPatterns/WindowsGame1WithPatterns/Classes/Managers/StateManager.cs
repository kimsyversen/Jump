using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Collections;
using System.Diagnostics;

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

        protected StateManager(Game game, string managerId, States stateId) : base(game)
        {
            //Add component to game loop.
            game.Components.Add(this);

            //All components is default off
            Enable(false);
            //Add newly created managers to the list (GameManager, MenuManager etc)
            StateManagers.Add(string.Concat(managerId, stateId.ToString()), this);

            _managerId = managerId;
        }

        public void Enable(bool value)
        {
            Visible = value;
            Enabled = value;
        }

        protected void ChangeStateTo(States stateId)
        {
            ChangeState = StateManagers[string.Concat(_managerId, stateId.ToString())];
        }

        protected enum States
        {
            InGame,
            MainMenu,
            GameOver,
        }
    }
}
