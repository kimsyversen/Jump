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
        
        private SpriteFactory _spriteFactory;
        private PlayerFactory _playerFactory;
        private FontFactory _fontFactory;
        private FloorFactory _floorFactory;

        private List<IPlayer> _players;
        private List<IFont> _fonts;
        private List<IFloor> _floors; 
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
            // TODO: Add your initialization logic here
            _spriteFactory = new SpriteFactory(this);
            _players = new List<IPlayer>();
            _fonts = new List<IFont>();
            _floors = new List<IFloor>();
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

            _playerFactory = _spriteFactory.CreatePlayerFactory();

            _floorFactory = _spriteFactory.CreateFloorFactory();

            var floor = _floorFactory.CreateFloorSprite();
            _floors.Add(floor);

            var player = _playerFactory.CreatePlayerOne();
            
             _players.Add(player);

            _fontFactory = _spriteFactory.CreateFontFactory();

            var font = _fontFactory.PlayerScoreFont(player);

            _fonts.Add(font);
            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            foreach (var player in _players)
            {
                player.Update(gameTime, Window.ClientBounds);

                foreach (var floor in _floors)
                    if (player.Collide.Intersects(floor.Collide))
                    {
                        player.PlayerSpeed = new Vector2(player.PlayerSpeed.X, 0f);
                    }
            }

            foreach (var font in _fonts)
                font.Update(gameTime, Window.ClientBounds);

            foreach (var floor in _floors)
                floor.Update(gameTime, Window.ClientBounds);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            foreach (var player in _players)
                player.Draw(gameTime, spriteBatch);

            foreach (var font in _fonts)
                font.Draw(gameTime, spriteBatch);

            foreach (var floor in _floors)
                floor.Draw(gameTime, spriteBatch);

           spriteBatch.End();

           base.Draw(gameTime);
        }
    }
}
