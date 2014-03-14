using WindowsGame1WithPatterns.Classes.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.State
{
    /// <summary>
    /// Manager for sub-classes of the state class
    /// </summary>
    class StateManager : GameComponent
    {
        /// <summary>
        /// Used to send down to the states
        /// </summary>
        private SpriteBatch _spriteBatch;

        /// <summary>
        /// Placeholder for the current state
        /// </summary>
        private State _currentState;

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
        public StateManager(Game game, SpriteBatch spriteBatch) : base(game)
        {
            _spriteBatch = spriteBatch;
            //Give the manager its uneque ID, and increment the naming
            //integer for the next manager to registrer its name.
            _stateManagerId = (_uniqueManagerId++).ToString();

            //Add StateManagers to game components
            game.Components.Add(this);
        }

        /// <summary>
        /// Method to initialize the values before gameloop begins
        /// </summary>
        public override void Initialize()
        {
            //Create all the states needed under the manager but remember 
            //the state the manager should start in. The StateManager
            //dont need to know about any of the other states but the
            //current state. The State class takes care of the rest.
            var menuManager = new MainMenu(Game, _spriteBatch, _stateManagerId, Game.Content.Load<SpriteFont>(@"Font\SimpleFont"), Game.Content.Load<Texture2D>(@"Figure\GameThumbnail"));
            menuManager.Hide();
            Game.Components.Add(menuManager);

            var optionsManager = new OptionsMenu(Game, _spriteBatch, _stateManagerId, Game.Content.Load<SpriteFont>(@"Font\SimpleFont"), Game.Content.Load<Texture2D>(@"Figure\Ball"));
            optionsManager.Hide();
            Game.Components.Add(optionsManager);

            var gameOverManager = new GameOver(Game, _spriteBatch, _stateManagerId, Game.Content.Load<SpriteFont>(@"Font\SimpleFont"), Game.Content.Load<Texture2D>(@"Figure\Ball"));
            gameOverManager.Hide();
            Game.Components.Add(gameOverManager);

            var highscoreManager = new HighscoreMenu(Game, _spriteBatch, _stateManagerId, Game.Content.Load<SpriteFont>(@"Font\SimpleFont"), Game.Content.Load<Texture2D>(@"Figure\Ball"));
            highscoreManager.Hide();
            Game.Components.Add(highscoreManager);

            var gameManager = new GameManager(Game, _spriteBatch, _stateManagerId);
            gameManager.Hide();
            Game.Components.Add(gameManager);

            //This must be added last on the Game.Components list because it
            //needs to be the last ting to be drawn. 
            var inGameMenuManager = new InGameMenu(Game, _spriteBatch, _stateManagerId, Game.Content.Load<SpriteFont>(@"Font\SimpleFont"), Game.Content.Load<Texture2D>(@"Figure\GameThumbnail"));
            inGameMenuManager.Hide();
            Game.Components.Add(inGameMenuManager);

            _currentState = menuManager;

            //At this point all states shall be hidden which is the responsibility 
            //of the programmer, therefore show the current state
            _currentState.Show();

            base.Initialize();
        }

        /// <summary>
        /// Method to constantly check for new states
        /// </summary>
        /// <param name="gameTime">GameManager time</param>
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
            var newState = _currentState.ChangeState;

            //If the newState is null, there will not be any state change,
            //and therefor we will return
            if (newState == null) return;

            //Disable the current state
            _currentState.Hide();
            //Change the state to the new state
            _currentState = newState;
            //Enable the new state
            _currentState.Show();
        }
    }
}
