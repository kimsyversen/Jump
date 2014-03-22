using WindowsGame1WithPatterns.Classes.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace WindowsGame1WithPatterns.Classes.State
{
    /// <summary>
    /// Manager for sub-classes of the state class
    /// </summary>
    class StateManager : GameComponent
    {
        /// <summary>
        /// Will remember all the paused states
        /// </summary>
        private Stack<State> _pausedStates;

        /// <summary>
        /// Used to be sendt down to the states
        /// </summary>
        private GraphicsDeviceManager _graphics;

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
        /// <param name="spriteBatch">Referance to the spriteBatch</param>
        /// <param name="graphics">Referance to the graphics</param>
        public StateManager(Game game, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
            : base(game)
        {
            _pausedStates = new Stack<State>();

            _graphics = graphics;

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
            var menuManager = new MainMenu(Game, _spriteBatch, _stateManagerId,
                Game.Content.Load<SpriteFont>(@"Font\SimpleFont"),
                Game.Content.Load<SpriteFont>(@"Font\HeaderFont"),
                Game.Content.Load<Texture2D>(@"Figure\GameThumbnail"));
            menuManager.Hide();
            Game.Components.Add(menuManager);

            var optionsManager = new OptionsMenu(Game, _spriteBatch, _stateManagerId, 
                Game.Content.Load<SpriteFont>(@"Font\SimpleFont"), 
                Game.Content.Load<Texture2D>(@"Figure\Ball"));
            optionsManager.Hide();
            Game.Components.Add(optionsManager);

            var gameOverManager = new GameOver(Game, _spriteBatch, _stateManagerId, 
                Game.Content.Load<SpriteFont>(@"Font\SimpleFont"),
                Game.Content.Load<SpriteFont>(@"Font\HeaderFont"),
                Game.Content.Load<Texture2D>(@"Figure\Ball"));
            gameOverManager.Hide();
            Game.Components.Add(gameOverManager);

            var newHighscoreManager = new NewHighscore(Game, _spriteBatch, _stateManagerId,
                Game.Content.Load<SpriteFont>(@"Font\SimpleFont"),
                Game.Content.Load<SpriteFont>(@"Font\HeaderFont"),
                Game.Content.Load<Texture2D>(@"Figure\Ball"));
            newHighscoreManager.Hide();
            Game.Components.Add(newHighscoreManager);

            var highscoreManager = new HighscoreMenu(Game, _spriteBatch, _stateManagerId, 
                Game.Content.Load<SpriteFont>(@"Font\SimpleFont"),
                Game.Content.Load<SpriteFont>(@"Font\HeaderFont"),
                Game.Content.Load<Texture2D>(@"Figure\Ball"));
            highscoreManager.Hide();
            Game.Components.Add(highscoreManager);

            var helpMenu = new HelpMenu(Game, _spriteBatch, _stateManagerId, 
                Game.Content.Load<SpriteFont>(@"Font\SimpleFont"),
                Game.Content.Load<SpriteFont>(@"Font\HeaderFont"),
                Game.Content.Load<Texture2D>(@"Figure\Ball"));
            helpMenu.Hide();
            Game.Components.Add(helpMenu);

            var choosePlayerManager = new ChoosePlayerManager(Game, _spriteBatch, _stateManagerId, 
                Game.Content.Load<SpriteFont>(@"Font\SimpleFont"), 
                Game.Content.Load<Texture2D>(@"Figure\GameThumbnail"));
            choosePlayerManager.Hide();
            Game.Components.Add(choosePlayerManager);

            var gameManager = new GameManager(Game, _spriteBatch, _stateManagerId, _graphics);
            gameManager.Hide();
            Game.Components.Add(gameManager);

            //This must be added last on the Game.Components list because it
            //needs to be the last ting to be drawn. 
            var inGameMenuManager = new InGameMenu(Game, _spriteBatch, _stateManagerId, 
                Game.Content.Load<SpriteFont>(@"Font\SimpleFont"), 
                Game.Content.Load<SpriteFont>(@"Font\HeaderFont"), 
                Game.Content.Load<Texture2D>(@"Figure\GameThumbnail"));
            inGameMenuManager.Hide();
            Game.Components.Add(inGameMenuManager);

            //Set the initial state
            _currentState = menuManager;

            //At this point all states shall be hidden which is the responsibility 
            //of the programmer, therefore show the current state
            _currentState.Show();

            base.Initialize();
        }

        /// <summary>
        /// Method to constantly check for new states
        /// </summary>
        /// <param name="gameTime">SingleplayerManager time</param>
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
            //be changed to. And if it should be a popUp
            //or not
            var newState = _currentState.ChangeState;
            var popUp = _currentState.PopUpState;

            //If the newState is null, there will not be any state change,
            //and therefore we will return
            if (newState == null) return;
            
            if (popUp && !_pausedStates.Contains(newState))
            {
                //Remember the current state
                _pausedStates.Push(_currentState);
                //Pause the current state
                _currentState.Pause();
                //Change the state to the new state
                _currentState = newState;
                //Show the new state
                _currentState.Show();
            }
            else
            {
                //Check if the newState is in the pause stack and hide the
                //states that is between the current and the new state
                State pausedState = null;
                while (_pausedStates.Count > 0)
                {
                    pausedState = _pausedStates.Pop();
                    if (pausedState == newState)
                        break;
                    else
                    {
                        //TODO: Redudant else
                        pausedState.Hide();
                        pausedState = null;
                    }
                }

                //Hide the current state
                _currentState.Hide();
                //set the new state to the current state
                _currentState = newState;

                //Resume the current state if it was found in the paused states
                //else start it from the begining through show()
                //If pausedState != null then the newState exists in the _pausedStates
                if (pausedState != null)
                    _currentState.Resume();
                else
                    _currentState.Show();
            }
        }
    }
}
