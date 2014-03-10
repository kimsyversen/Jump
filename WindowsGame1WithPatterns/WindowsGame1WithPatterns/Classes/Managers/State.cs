using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace WindowsGame1WithPatterns.Classes.Managers
{
    /// <summary>
    /// Abstract state class
    /// </summary>
    abstract class State : DrawableGameComponent
    {
        /// <summary>
        /// All the states in the game are in the dictionary States
        /// </summary>
        private static readonly Dictionary<string, State> States = new Dictionary<string, State>();

        /// <summary>
        /// _managerId is to identify the state manager the state is controlled by
        /// </summary>
        private readonly string _managerId;

        /// <summary>
        /// See ChangeState property
        /// </summary>
        private State _changeState;

        /// <summary>
        /// ChangeState is the property that the state manager gets
        /// to change the state. When a state (sub-class of this) 
        /// wants to change the state, it calls the method ChangeStateTo
        /// with the apropriate enum of the state
        /// </summary>
        public State ChangeState
        {
            get
            {
                State changingState = _changeState;
                _changeState = null;
                return changingState;
            }
            private set
            {
                _changeState = value; 
            }
        }

        /// <summary>
        /// State constructor
        /// </summary>
        /// <param name="game">Referance to the game</param>
        /// <param name="managerId">Unique ID to the manager of this state</param>
        /// <param name="gameStateId">Unique enum under the manager used to identify the state</param>
        protected State(Microsoft.Xna.Framework.Game game, string managerId, GameStates gameStateId) : base(game)
        {
            //Add component to game loop.
            game.Components.Add(this);

            //All components is default off
            Enable(false);
            //Add newly created managers to the list (Game, MenuManager etc)
            States.Add(string.Concat(managerId, gameStateId.ToString()), this);

            //Store the managerId for lookup in the dictionary
            _managerId = managerId;
        }

        /// <summary>
        /// Enable or disable the states draw and update method
        /// </summary>
        /// <param name="value">When true the state is enabled, when false the state is disabled</param>
        public void Enable(bool value)
        {
            Visible = value;
            Enabled = value;
        }

        /// <summary>
        /// When a state (sub-class of this) 
        /// wants to change the state, it calls the method ChangeStateTo
        /// with the apropriate enum of the state
        /// </summary>
        /// <param name="gameStateId">Enum identifying the states</param>
        protected void ChangeStateTo(GameStates gameStateId)
        {
            Debug.WriteLine("Changing state to: " + gameStateId.ToString());
            ChangeState = States[string.Concat(_managerId, gameStateId.ToString())];
        }

        /// <summary>
        /// The states available to all sub-classes of this class.
        /// To create a new state, an enum must be apointed to the state.
        /// </summary>
        protected enum GameStates
        {
            InGame,
            MainMenu,
            GameOver,
        }
    }
}
