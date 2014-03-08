using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using WindowsGame1WithPatterns.Classes;
using WindowsGame1WithPatterns.Classes.KeyboardConfiguration;
using WindowsGame1WithPatterns.Classes.Managers;

using WindowsGame1WithPatterns.Classes.Sprites;
using WindowsGame1WithPatterns.Classes.Sprites.Factories;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Floors;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player;

namespace WindowsGame1WithPatterns
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;



        private KeyboardState _oldKeyState;
        private KeyboardState _newKeyState;


        private Manager _manager;

     

        

        private KeyboardConfiguration _keyboard;
      
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            
            //_inGameManager = new InGameManager(this);
            //_menuManager = new MenuManager(this);
            _manager = new Manager(this);

           // Components.Add(_inGameManager);
          //  Components.Add(_menuManager);

            _oldKeyState = Keyboard.GetState();

            _keyboard = new KeyboardConfiguration(Keys.None, Keys.Up, Keys.None, Keys.Enter, Keys.Escape, Keys.None, Keys.None);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            base.LoadContent();
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
            //Need to to this because updating of frames happens faster than a user releases a key
            //Without, it would just switch fast between menu and game
            _newKeyState = Keyboard.GetState();

            //User is pressing Escape down
            if (_newKeyState.IsKeyDown(_keyboard.Back) && !_oldKeyState.IsKeyDown(_keyboard.Back))
                    // If not down last update, key has just been pressed.
                if (!_oldKeyState.IsKeyDown(_keyboard.Back))
                        _manager.SwitchState();
                    else
                        // Key was down last update, but not down now, so
                        // it has just been released.
                        // the player just pressed down
                    _manager.SwitchState();

            switch (_manager.CurrentGameState)
            {
                case GameState.InMenu:
                    if(_manager.OldGameState != GameState.InMenu)
                        _manager.SwitchState(true);
                    break;
                case GameState.InGame:
                    if (_manager.OldGameState != GameState.InGame)
                        _manager.SwitchState(false);
                    break;
                case GameState.GameOver:
                    break;
            }

            //Store the old state
            _oldKeyState = _newKeyState;
            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
