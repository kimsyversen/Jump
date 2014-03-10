using Microsoft.Xna.Framework;

namespace WindowsGame1WithPatterns.Classes.Managers
{
    /// <summary>
    /// Manager for sub-classes of the state class
    /// </summary>
    class StateManager : DrawableGameComponent
    {
        /// <summary>
        /// Placeholder for the current state
        /// </summary>
        protected State CurrentState;

        /// <summary>
        /// Id to unequely identify the state manager
        /// </summary>
        private string _stateManagerId;

        /// <summary>
        /// An incrementing static integer to give all StateManagers uneque names
        /// </summary>
        private static int _uniqueManagerId;

        /// <summary>
        /// StateManagers constructor
        /// </summary>
        /// <param name="game">Referance to the game</param>
        public StateManager(Microsoft.Xna.Framework.Game game) : base(game)
        {
            //Add StateManagers to game components
            game.Components.Add(this);
        }

        /// <summary>
        /// Method to initialize the values before gameloop begins
        /// </summary>
        public override void Initialize()
        {
            //StateManagers is not ment to be visible, so the draw method
            //will be disabled.
            Visible = false;

            //Give the manager its uneque ID, and increment the naming
            //integer for the next manager to registrer its name.
            _stateManagerId = (_uniqueManagerId++).ToString(); 

            //Create all the states needed under the manager but remember 
            //the state the manager should start in. The StateManager
            //dont need to know about any of the other states but the
            //current state. The State class takes care of the rest.
            CurrentState = new MenuManager(Game, _stateManagerId);
            new Game(Game, _stateManagerId);
            new GameOver(Game, _stateManagerId);

            //Start the game in the menu
            //CurrentState = _menu;

            //Since all managers are default off, enable the one that shall be started
            CurrentState.Enable(true);

            base.Initialize();
        }

        /// <summary>
        /// Method to constantly check for new states
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Update(GameTime gameTime)
        {
            CheckForNewState();
            base.Update(gameTime);
        }

        /// <summary>
        /// Ask the current state if the state needs to be changed
        /// and change the state accordingly
        /// </summary>
        private void CheckForNewState()
        {
            //Holds the instance of the state that is to
            //be changed to.
            var newState = CurrentState.ChangeState;

            //If the newState is null, there will not be any state change,
            //and therefor we will return
            if (newState == null) return;

            //Disable the current state
            CurrentState.Enable(false);
            //Change the state to the new state
            CurrentState = newState;
            //Enable the new state
            CurrentState.Enable(true);
        }
    }
}
