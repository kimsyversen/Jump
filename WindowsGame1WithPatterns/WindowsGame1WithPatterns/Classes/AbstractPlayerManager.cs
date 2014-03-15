using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WindowsGame1WithPatterns.Classes.CameraConfiguration;
using WindowsGame1WithPatterns.Classes.Sprites.Concretes;
using WindowsGame1WithPatterns.Classes.KeyboardConfiguration;

namespace WindowsGame1WithPatterns.Classes
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    abstract class AbstractPlayerManager : State.State
    {
        private Texture2D _background;
        private Rectangle _mainFrame;
        private CameraManager _camera;
        private List<Vector2> _playerPosition;

        private List<Player> _players;
        private List<Font> _fonts;
        private List<Platform> _floors;

        //For platform generation and camera following
        private int _heightOfBoard = 0;
        private int _minPlatFormWidth = 100;
        private const int MinDistance = 20;
        private int _maxDistance = 100;
        private int _numberOfPlatforms = 20;
        private const int HeightBetweenPlatforms = 70;
        private int _level = 1;
        private readonly GraphicsDeviceManager _graphics;


        private const int DifficulityFactor = 20;

        private readonly int _numberOfPlayers;

        protected AbstractPlayerManager(Game game, SpriteBatch spriteBatch, string managerId, GameStates stateId, GraphicsDeviceManager graphics, int numberOfPlayers)
            : base(game, spriteBatch, managerId, stateId)
        {
            _graphics = graphics;
            _numberOfPlayers = numberOfPlayers;
        }

        public override void Initialize()
        {

            _players = new List<Player>();
            _fonts = new List<Font>();
            _floors = new List<Platform>();
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
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _floors.Add(new Platform(_game, 0, (_game.Window.ClientBounds.Height) - 5,
                                                  _game.Window.ClientBounds.Width, 5));


            //Singleplayer
            _players.Add(new Player(_game, "Figure/lilastoy", new KeyboardMapping(Keys.A, Keys.D, Keys.Space), 
                new Vector2(_game.Window.ClientBounds.Width / 2f, _game.Window.ClientBounds.Height)));

            //If multiplayer
            if (_numberOfPlayers > 1)
                _players.Add(new Player(_game, "Figure/greenstoy", new KeyboardMapping(Keys.Left, Keys.Right, Keys.Up), 
                    new Vector2(_game.Window.ClientBounds.Width / 3f, _game.Window.ClientBounds.Height)));


            _camera = new CameraManager(GraphicsDevice.Viewport, -0.1f, _graphics.PreferredBackBufferHeight);

            GeneratePlatforms(_numberOfPlatforms, MinDistance, _maxDistance, _minPlatFormWidth);

            _background = _game.Content.Load<Texture2D>(@"Figure\bg");
            _mainFrame = new Rectangle(0, -100000, GraphicsDevice.Viewport.Width, 110000);
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
            if (InputManager.Instance.IsKeyPressed(Keys.Escape))
                ChangeStateTo(GameStates.InGameMenu);

            foreach (var player in _players)
            {
                player.Update(gameTime, _game.Window.ClientBounds);
                _playerPosition.Add(player.Position);
                _camera.Update(_playerPosition, _game.Window.ClientBounds.Width, _heightOfBoard, gameTime);
                foreach (var floor in _floors)
                {
                    //Sjekker om spilleren har truffet en platform
                    if (player.CollisionRectangle.Intersects(floor.CollisionRectangle) && player.HasHitPlatform == false && (player.GetY + player.Texture.Height) < floor.Position.Y)
                    {
                        player.LandedOnPlatForm(floor);
                        _camera.StartCam = true;
                    }
                    //Sjekker om spilleren har gått av platformen
                    if (player.HasHitPlatform && !player.CollisionRectangle.Intersects(floor.CollisionRectangle) && floor == player.OnFloor)
                        player.WalkedOfPlatform();

                }

                //Sjekker om alle spillerne er utenfor brettet/synsvinkel
                if (GameOver(_players, _camera.Center))
                {
                    ChangeStateTo(GameStates.GameOver);
                    _camera.StartCam = false;
                }

                //Sjekker om spilleren er ferdig med en level, isåfall starter en ny, vanskeligere en.
                if (player.Position.Y < _floors[_floors.Count - 1].Position.Y)
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

            if (_maxDistance < 225)
                _maxDistance = _maxDistance + DifficulityFactor;

            _numberOfPlatforms = _numberOfPlatforms + DifficulityFactor / 4;

            GeneratePlatforms(_numberOfPlatforms, MinDistance, _maxDistance, _minPlatFormWidth);

            _camera.IncreaseSpeed();

            _level++;
        }
        protected void GeneratePlatforms(int antall, int minDistance, int maxDistance, int minWidth)
        {
            var rnd = new Random();
            int teller = 0;

            while (teller < antall)
            {
                Platform floor;
                int x = rnd.Next(minDistance, maxDistance);
                int width = rnd.Next(minWidth, 100);
                int test = rnd.Next(1, 3);

                //public Platform(Game game, float x, float y, int width, int height)
                if (teller + 1 == antall)
                    floor = new Platform(_game, 1,
                       _floors[_floors.Count - 1].Position.Y - HeightBetweenPlatforms, _game.Window.ClientBounds.Width - 1, 5);

                else if (test == 1)
                    floor = new Platform(_game, _floors[_floors.Count - 1].Position.X - x,
                                         _floors[_floors.Count - 1].Position.Y - HeightBetweenPlatforms, width, 5);

                else
                    floor = new Platform(_game, _floors[_floors.Count - 1].Position.X + x, _floors[_floors.Count - 1].Position.Y - HeightBetweenPlatforms, width, 5);

                if (!CheckOutsideRange(floor))
                {
                    _floors.Add(floor);
                    teller++;
                }
            }
            _heightOfBoard = HeightBetweenPlatforms * _floors.Count;
        }

        private bool CheckOutsideRange(Platform floor)
        {
            if (floor.Position.X <= 0 || floor.Position.X + floor.Texture.Width >= _game.Window.ClientBounds.Width)
                return true;

            return false;
        }

        private bool GameOver(IEnumerable<Player> players, Vector2 center)
        {
            foreach (Player p in players)
                if (!(p.Position.Y > center.Y + _game.Window.ClientBounds.Height / 2f))
                    return false;
            return true;
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred,
                         BlendState.AlphaBlend,
                         null, null, null, null,
                         _camera.Transform);
            _spriteBatch.Draw(_background, _mainFrame, Color.White);

            foreach (var player in _players)
                player.Draw(gameTime, _spriteBatch);

            foreach (var font in _fonts)
                font.Draw(gameTime, _spriteBatch);

            foreach (var floor in _floors)
                floor.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

