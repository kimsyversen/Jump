using System;
using WindowsGame1WithPatterns.Classes.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame1WithPatterns.Classes.KeyboardConfiguration;
namespace WindowsGame1WithPatterns
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private StateManager _stateManager;
        private InputManager _inputManager;
        public GraphicsDeviceManager Graphics
        {
            get { return _graphics; }
        }
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //15% offset in height 
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 
                Convert.ToInt32(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.15);
            
            _graphics.ApplyChanges();

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // Initialize the state manager so it can handle the game states
            _stateManager = new StateManager(this, _spriteBatch, _graphics);
            // Initialize input manager so the game can accept input from keyboard
            _inputManager = InputManager.Instance;

            base.Initialize();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            
            // Update input manager
            _inputManager.Begin();
            base.Update(gameTime);
            _inputManager.End();
        }

    }
}
