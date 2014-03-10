using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using WindowsGame1WithPatterns.Classes.CameraConfiguration;
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
        private CameraManager camera;

        private List<IPlayer> _players;
        private List<IFont> _fonts;
        private List<IFloor> _floors;
        private int height = 0;
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
            var player2 = _playerFactory.CreatePlayerTwo();
            
             _players.Add(player);
             _players.Add(player2);
            _fontFactory = _spriteFactory.CreateFontFactory();

            var font = _fontFactory.PlayerScoreFont(player);
            camera = new CameraManager(GraphicsDevice.Viewport);
            _fonts.Add(font);
            generatePlatforms(15,200);
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
        /// 
        /// 

        private void test()
        {

           // camera.
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            //camera.Update(_players[0].PlayerPosition,Window.ClientBounds.Width,height);
            foreach (var player in _players)
            {
                player.Update(gameTime, Window.ClientBounds);
                
                foreach (var floor in _floors)
                {
                    //Sjekker om spilleren har truffet en platform
                    if (player.Collide.Intersects(floor.Collide) && player.HasHitPlatform == false && (player.GetY + player.PlayerTexture.Height)<floor.FloorPosition.Y)
                    {
                        player.LandedOnPlatForm(floor);
                    }
                    //Sjekker om spilleren har gått av platformen
                    if (player.HasHitPlatform && !player.Collide.Intersects(floor.Collide) && floor == player.OnFloor)
                    {
                        player.WalkedOfPlatform();
                    }                    
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
        /// 
        
        protected void generatePlatforms(int antall, int maxDistance)
        {
            Random rnd = new Random();
            int teller = 0;
            IFloor floor;
            int hoyde = 50;
            height = hoyde * antall;
            Console.WriteLine(height);
            while (teller < antall)
            {
                int y = rnd.Next(20, maxDistance);
                int test = rnd.Next(1, 3);
                if (test == 1)
                {
                    floor = _floorFactory.CreateFloorSpriteInputs(_floors[_floors.Count - 1].FloorPosition.X - y,
                        _floors[_floors.Count - 1].FloorPosition.Y - hoyde, 100, 5);
                }
                else
                {
                    floor = _floorFactory.CreateFloorSpriteInputs(_floors[_floors.Count - 1].FloorPosition.X + y, _floors[_floors.Count - 1].FloorPosition.Y - hoyde, 100, 5);
                }
                if (!CheckOutsideRange(floor))
                {
                    _floors.Add(floor);
                    teller++;
                }         
            }            
        }

        private bool CheckOutsideRange(IFloor floor)
        {
            if (floor.FloorPosition.X <= 0 || floor.FloorPosition.X + floor.FloorTexture.Width >= Window.ClientBounds.Width)
            {
                return true;
            }
            return false;
        }

        // Høyde: 50, lengste hopp: 220
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

           /* spriteBatch.Begin(SpriteSortMode.Deferred,
                        BlendState.AlphaBlend,
                        null, null, null, null,
                        camera.Transform);*/

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
