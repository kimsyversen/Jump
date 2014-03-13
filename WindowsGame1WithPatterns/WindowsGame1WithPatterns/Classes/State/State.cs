using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.States
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
            private set
            {
                _changeState = value; 
            }
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
        /// <param name="managerId">Unique ID to the manager of this state</param>
        /// <param name="gameStateId">Unique enum under the manager used to identify the state</param>
        protected State(Game game, SpriteBatch spriteBatch, 
            string managerId, GameStates gameStateId) 
            : base(game)
        {
            //Add newly created managers to the list (GameManager, MenuManager etc)
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
            this.Visible = true;
            this.Enabled = true;
            foreach (GameComponent component in _components)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = true;
            }
        }

        /// <summary>
        /// Sets the Enable and Visible properties to false so the 
        /// component will not be updated or drawn
        /// </summary>
        public virtual void Hide()
        {
            this.Visible = false;
            this.Enabled = false;
            foreach (GameComponent component in _components)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = false;
            }
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
            Highscore,
            Options,
            InGameMenu,
        }
    }
}