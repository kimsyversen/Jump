using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.State
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
        /// Will be used by StateManager to determine if the
        /// _changeState will be a pop up or not
        /// </summary>
        private bool _popUpState;

        /// <summary>
        /// Will hold any child components of the screen
        /// </summary>
        private List<GameComponent> _components = new List<GameComponent>();
        
        /// <summary>
        /// Will hold the current Game object
        /// </summary>
        protected Game _game;

        /// <summary>
        /// Will be able to use to draw in inherited classes
        /// </summary>
        protected SpriteBatch _spriteBatch;

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
            private set { _changeState = value; }
        }

        /// <summary>
        /// Will be used by StateManager to determine if the
        /// _changeState will be a pop up or not. When a state
        /// (sub-class of this) want a pop-up state, it calls the
        /// method PupUp with the apropriate enum of the state
        /// </summary>
        public bool PupUpState
        {
            get
            {
                bool popUp = _popUpState;
                _popUpState = false;
                return popUp;
            }
            private set { _popUpState = value; }
        }

        /// <summary>
        /// Used in child classes and classes that use the screen to 
        /// get the list of components of the screen
        /// </summary>
        public List<GameComponent> Components
        {
            get { return _components; }
        }

        /// <summary>
        /// State constructor
        /// </summary>
        /// <param name="game">Referance to the game</param>
        /// <param name="spriteBatch">Referance to the spriteBatch</param>
        /// <param name="managerId">Unique ID to the manager of this state</param>
        /// <param name="gameStateId">Unique enum under the manager used to identify the state</param>
        protected State(Game game, SpriteBatch spriteBatch, 
            string managerId, GameStates gameStateId) 
            : base(game)
        {
            //Add newly created managers to the list (SingleplayerManager, MenuManager etc)
            States.Add(string.Concat(managerId, gameStateId.ToString()), this);

            //Store the managerId for lookup in the dictionary
            _managerId = managerId;

            //Keep a referance to the game and spriteBatch so that sub classes 
            //can access them
            _game = game;
            _spriteBatch = spriteBatch;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Update the components if they are enabled
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (GameComponent component in _components)
                if (component.Enabled)
                    component.Update(gameTime);
        }

        /// <summary>
        /// Draw the component if it is of type DrawableGameComponent and
        /// is visible
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            foreach (GameComponent component in _components)
                if (component is DrawableGameComponent &&
                    ((DrawableGameComponent)component).Visible)
                    ((DrawableGameComponent)component).Draw(gameTime);
        }

        /// <summary>
        /// Used to set the component to be enabled and visible
        /// </summary>
        public virtual void Show()
        {
            Visible = true;
            Enabled = true;
            foreach (GameComponent component in _components)
            {
                component.Enabled = true;
                var gameComponent = component as DrawableGameComponent;
                if (gameComponent != null)
                    gameComponent.Visible = true;
            }
        }

        /// <summary>
        /// Sets the Enable and Visible properties to false so the 
        /// component will not be updated or drawn
        /// </summary>
        public virtual void Hide()
        {
            Visible = false;
            Enabled = false;
            foreach (GameComponent component in _components)
            {
                component.Enabled = false;
                var gameComponent = component as DrawableGameComponent;
                if (gameComponent != null)
                    gameComponent.Visible = false;
            }
        }

        /// <summary>
        /// Pause the state
        /// </summary>
        public virtual void Pause()
        {
            
        }

        /// <summary>
        /// Stop the state
        /// </summary>
        public virtual void Resume()
        {
            
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
            PupUpState = false;
        }

        /// <summary>
        /// When a state (sub-class of this) 
        /// wants a state to pup up, it calls the method PopUp
        /// with the apropriate enum of the state
        /// </summary>
        /// <param name="gameStateId"></param>
        protected void PopUp(GameStates gameStateId)
        {
            ChangeState = States[string.Concat(_managerId, gameStateId.ToString())];
            PupUpState = true;
        }

        /// <summary>
        /// Get the state with the specified GameState
        /// </summary>
        /// <param name="gameStateId">GameState id of the state</param>
        /// <returns>Returns the state</returns>
        protected State GetState(GameStates gameStateId)
        {
            return States[string.Concat(_managerId, gameStateId.ToString())];
        }

        /// <summary>
        /// The states available to all sub-classes of this class.
        /// To create a new state, an enum must be apointed to the state.
        /// </summary>
        protected enum GameStates
        {
            MainMenu,
            GameOver,
            Highscore,
            Options,
            InGameMenu,
            MultiplayerManager,
            SingleplayerManager,
            ChoosePlayerManager,
            HelpMenu,
        }
    }
}