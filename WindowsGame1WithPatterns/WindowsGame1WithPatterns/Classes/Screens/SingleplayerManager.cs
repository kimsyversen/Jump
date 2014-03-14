using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
//using WindowsGame1WithPatterns.Classes.CameraConfiguration;
using System.Xml.Linq;
using WindowsGame1WithPatterns.Classes.KeyboardConfiguration;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Platform;
using WindowsGame1WithPatterns.Classes.State;
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
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Platform;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player;
using WindowsGame1WithPatterns.Classes.CameraConfiguration;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;


namespace WindowsGame1WithPatterns.Classes.Screens
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
     class SingleplayerManager : State.State//, Microsoft.Xna.Framework
    {
        private GraphicsDeviceManager _graphics;
        SpriteBatch spriteBatch;
        private Texture2D background;
        private Rectangle mainFrame;

        private SpriteFactory _spriteFactory;
        private PlayerFactory _playerFactory;
        private FontFactory _fontFactory;
        private PlatformFactory _platformFactory;
        private CameraManager _camera;
        private List<Vector2> _playerPosition;

        private List<IPlayer> _players;
        private List<IFont> _fonts;
        private List<IPlatform> _floors;


        //For platform generation and camera following
        private int _heightOfBoard = 0;
        private int _minPlatFormWidth = 100;
        private const int _minDistance = 20;
        private int _maxDistance = 100;
        private int _numberOfPlatforms = 20;
        private const int HeightBetweenPlatforms = 70;
         
        //TODO: REMOVE?
        private int _level = 1;
     


        private const int DifficulityFactor = 20;



        public SingleplayerManager(Game game, SpriteBatch spriteBatch, string managerId, GraphicsDeviceManager graphics)
            : base(game, spriteBatch, managerId, GameStates.SingleplayerManager)
        {
            _graphics = graphics;
        }

        public override void Initialize()
        {

            _spriteFactory = new SpriteFactory(_game);
            _players = new List<IPlayer>();
            _fonts = new List<IFont>();
            _floors = new List<IPlatform>();
            _playerPosition = new List<Vector2>();
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

            _platformFactory = _spriteFactory.CreatePlatformFactory();

            var floor = _platformFactory.CreateFloorSprite();
            _floors.Add(floor);

            var player = _playerFactory.CreatePlayerOne();

            _players.Add(player);
         
            _fontFactory = _spriteFactory.CreateFontFactory();

            _camera = new CameraManager(GraphicsDevice.Viewport, -0.1f, _graphics.PreferredBackBufferHeight);
          
            GeneratePlatforms(_numberOfPlatforms, _minDistance, _maxDistance, _minPlatFormWidth);

            background = _game.Content.Load<Texture2D>(@"Figure\bg");
            mainFrame = new Rectangle(0, -100000, GraphicsDevice.Viewport.Width, 110000);
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


        public override void Update(GameTime gameTime)
        {
           if(InputManager.Instance.IsKeyPressed(Keys.Escape))
               ChangeStateTo(GameStates.InGameMenu);

            foreach (var player in _players)
            {
                player.Update(gameTime, _game.Window.ClientBounds);
                _playerPosition.Add(player.PlayerPosition);
                _camera.Update(_playerPosition, _game.Window.ClientBounds.Width, _heightOfBoard, gameTime);
                foreach (var floor in _floors)
                {
                    //Sjekker om spilleren har truffet en platform
                    if (player.Collide.Intersects(floor.Collide) && player.HasHitPlatform == false && (player.GetY + player.PlayerTexture.Height) < floor.FloorPosition.Y)
                    {
                        player.LandedOnPlatForm(floor);
                        _camera.StartCam = true;
                    }
                    //Sjekker om spilleren har gått av platformen
                    if (player.HasHitPlatform && !player.Collide.Intersects(floor.Collide) && floor == player.OnFloor)
                        player.WalkedOfPlatform();
                    
                }

                //Sjekker om alle spillerne er utenfor brettet/synsvinkel
                if (GameOver(_players, _camera.Center))
                {
                    ChangeStateTo(GameStates.GameOver);
                    _camera.StartCam = false;
                }

                //Sjekker om spilleren er ferdig med en level, isåfall starter en ny, vanskeligere en.
                if (player.PlayerPosition.Y < _floors[_floors.Count - 1].FloorPosition.Y)
                    LevelUp();
                
            }
            _playerPosition = new List<Vector2>();
            foreach (var font in _fonts)
                font.Update(gameTime, _game.Window.ClientBounds);

            foreach (var floor in _floors)
                floor.Update(gameTime, _game.Window.ClientBounds);

            base.Update(gameTime);
        }


        protected void LevelUp()
        {
            if (_minPlatFormWidth > 10)
                _minPlatFormWidth = _minPlatFormWidth - DifficulityFactor / 2;

            /* if(_minDistance<200)
             _minDistance = _minDistance + DifficulityFactor;*/

            if (_maxDistance < 225)
                _maxDistance = _maxDistance + DifficulityFactor;

            _numberOfPlatforms = _numberOfPlatforms + DifficulityFactor / 4;

            GeneratePlatforms(_numberOfPlatforms, _minDistance, _maxDistance, _minPlatFormWidth);

            _camera.IncreaseSpeed();

            _level++;
        }
        protected void GeneratePlatforms(int antall, int minDistance, int maxDistance, int minWidth)
        {
            var rnd = new Random();
            int teller = 0;

            while (teller < antall)
            {
                IPlatform floor;
                int x = rnd.Next(minDistance, maxDistance);
                int width = rnd.Next(minWidth, 100);
                int test = rnd.Next(1, 3);

                if (teller + 1 == antall)
                    floor = _platformFactory.CreateFloorSpriteInputs(1,
                       _floors[_floors.Count - 1].FloorPosition.Y - HeightBetweenPlatforms, _game.Window.ClientBounds.Width - 1, 5);
                
                else if (test == 1)
                    floor = _platformFactory.CreateFloorSpriteInputs(_floors[_floors.Count - 1].FloorPosition.X - x,
                        _floors[_floors.Count - 1].FloorPosition.Y - HeightBetweenPlatforms, width, 5);
                
                else
                    floor = _platformFactory.CreateFloorSpriteInputs(_floors[_floors.Count - 1].FloorPosition.X + x, _floors[_floors.Count - 1].FloorPosition.Y - HeightBetweenPlatforms, width, 5);

                if (!CheckOutsideRange(floor))
                {
                    _floors.Add(floor);
                    teller++;
                }
            }
            _heightOfBoard = HeightBetweenPlatforms * _floors.Count;
        }

        private bool CheckOutsideRange(IPlatform floor)
        {
            if (floor.FloorPosition.X <= 0 || floor.FloorPosition.X + floor.FloorTexture.Width >= _game.Window.ClientBounds.Width)
                return true;
            
            return false;
        }

        private bool GameOver(List<IPlayer> players, Vector2 center)
        {
            foreach (IPlayer p in players)
                if (!(p.PlayerPosition.Y > center.Y + _game.Window.ClientBounds.Height / 2f))
                    return false;
            return true;
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred,
                         BlendState.AlphaBlend,
                         null, null, null, null,
                         _camera.Transform);
            spriteBatch.Draw(background, mainFrame, Color.White);

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

