﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WindowsGame1WithPatterns.Classes.CameraConfiguration;
using WindowsGame1WithPatterns.Classes.Sprites.Concretes;
using WindowsGame1WithPatterns.Classes.KeyboardConfiguration;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1WithPatterns.Classes
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    abstract class AbstractGameManager : State.State
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

        private int _platformWidth = 100;

        private const int MinDistance = 20;

        private int _maxDistance = 100;

        private int _numberOfPlatforms = 20;

        private const int MinimumPlatformWidth = 10;
        private const int HeightBetweenPlatforms = 70;
        private const int DistanceBetweenPlatforms = 225;

        private int _level = 1;

        private readonly GraphicsDeviceManager _graphics;

        private const int DifficulityFactor = 20;

        private readonly int _numberOfPlayers;

        private const int HeightOfPlatform = 5;

        private Random _randomNumber;

        private Song _startGameSong;

        protected AbstractGameManager(Game game, SpriteBatch spriteBatch, string managerId, GameStates stateId, GraphicsDeviceManager graphics, int numberOfPlayers)
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
            _floors.Add(new Platform(_game, 0, (_game.Window.ClientBounds.Height) - HeightOfPlatform,
                                                  _game.Window.ClientBounds.Width, HeightOfPlatform));

            //Singleplayer
            _players.Add(new Player(_game, "Figure/lilastoy", new KeyboardMapping(Keys.A, Keys.D, Keys.Space), 
                new Vector2(_game.Window.ClientBounds.Width / 2f, _game.Window.ClientBounds.Height)));

            //If multiplayer
            if (_numberOfPlayers > 1)
                _players.Add(new Player(_game, "Figure/greenstoy", new KeyboardMapping(Keys.Left, Keys.Right, Keys.Up), 
                    new Vector2(_game.Window.ClientBounds.Width / 2f, _game.Window.ClientBounds.Height)));

            _camera = new CameraManager(GraphicsDevice.Viewport, -0.1f, _graphics.PreferredBackBufferHeight);

            GeneratePlatforms(_numberOfPlatforms, MinDistance, _maxDistance, _platformWidth);

            _background = _game.Content.Load<Texture2D>(@"Figure\bg");

            _startGameSong = _game.Content.Load<Song>("Audio/StartGameSong");

            //TODO: 110000 og -100000 er?
            _mainFrame = new Rectangle(0, -100000, GraphicsDevice.Viewport.Width, 110000);
            base.LoadContent();
        }

        public override void Show()
        {
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_startGameSong);
            base.Show();
        }

        public override void Hide()
        {
            MediaPlayer.Stop();
            base.Hide();
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
            _playerPosition = new List<Vector2>();

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

            foreach (var font in _fonts)
                font.Update(gameTime, _game.Window.ClientBounds);

            foreach (var floor in _floors)
                floor.Update(gameTime, _game.Window.ClientBounds);

            base.Update(gameTime);
        }

        protected void LevelUp()
        {
            //TODO: Kan 2 og 4 gjøres om til variabel med forklarende navn?
            if (_platformWidth > MinimumPlatformWidth)
                _platformWidth = _platformWidth - DifficulityFactor / 2;

            if (_maxDistance < DistanceBetweenPlatforms)
                _maxDistance = _maxDistance + DifficulityFactor;

            _numberOfPlatforms = _numberOfPlatforms + DifficulityFactor / 4;

            GeneratePlatforms(_numberOfPlatforms, MinDistance, _maxDistance, _platformWidth);

            _camera.IncreaseSpeed();

            _level++;
        }

        protected void GeneratePlatforms(int numberOfPlatforms, int minDistance, int maxDistance, int minWidth)
        {
            _randomNumber = new Random();

            for (var i = 0; i < numberOfPlatforms; i++)
            {
                Platform floor;
                //TODO: Hva er x?
                var x = _randomNumber.Next(minDistance, maxDistance);
                var width = _randomNumber.Next(minWidth, _platformWidth);

                //TODO: Hva er test?
                var test = _randomNumber.Next(1, 3);

                if (i + 1 == numberOfPlatforms)
                    floor = new Platform(_game, 1,
                       _floors[_floors.Count - 1].Position.Y - HeightBetweenPlatforms,
                       _game.Window.ClientBounds.Width, HeightOfPlatform);

                else if (test == 1)
                    floor = new Platform(_game, _floors[_floors.Count - 1].Position.X - x,
                                         _floors[_floors.Count - 1].Position.Y - HeightBetweenPlatforms, width, HeightOfPlatform);
                else
                    floor = new Platform(_game, _floors[_floors.Count - 1].Position.X + x, _floors[_floors.Count - 1].Position.Y - HeightBetweenPlatforms, width, HeightOfPlatform);

                if (CheckOutsideRange(floor))
                    continue;

                _floors.Add(floor);
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
