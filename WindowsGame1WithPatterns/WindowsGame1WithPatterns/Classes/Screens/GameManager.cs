﻿using System;
using System.Collections.Generic;
using System.Linq;
using Jump.Classes.CameraConfiguration;
using Jump.Classes.KeyboardConfiguration;
using Jump.Classes.Sprites.Concretes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Jump.Classes.Screens
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    class GameManager : State.State
    {
        private Texture2D _background;
        private Rectangle _mainFrame;
        private CameraManager _camera;
        private List<Vector2> _playerPosition;
        private List<Player> _players;
        private List<Font> _fonts;
        private List<Platform> _platforms;
        private Random _random;
        private Song _startGameSong;
        private String _fontString = "Initially empty";
        private int _platformWidth;
        private int _maxDistance;
        private int _numberOfPlatforms;
        private float _gameVelocity;
        private int _numberOfPlayers;

        private const int MinimumDistancePlatform = 20;
        private const int MinimumPlatformWidth = 10;
        private const int HeightBetweenPlatforms = 70;
        private const int DistanceBetweenPlatforms = 225;
        private const int HeightOfPlatform = 5;
        private const int DifficulityFactor = 20;
        private const float MaximumSpeedLimit = 5.0f;
        private const int BackgroundImageYMinimum = -100000;
        private const int BackgroundImageYMaximum = 110000;
        private const int MaximumPlatformWidth = 100;
        /// <summary>
        /// Start velocity for camera
        /// </summary>
        private const float CameraVelocity = -0.1f;

        public GameManager(Game game, SpriteBatch spriteBatch, string managerId, GraphicsDeviceManager graphics)
            : base(game, spriteBatch, managerId, GameStates.GameManager)
        {
  
        }
        public void NumberOfPlayers(int numberOfPlayers)
        {
            _numberOfPlayers = numberOfPlayers;
        }

        public override void Initialize()
        {
            _players = new List<Player>();
            _fonts = new List<Font>();
            _platforms = new List<Platform>();
            _playerPosition = new List<Vector2>();
            _random = new Random();
            _platformWidth = 100;
            _maxDistance = 100;
            _numberOfPlatforms = 20;
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
            _platforms.Add(new Platform(_game, 0, (_game.Window.ClientBounds.Height) - HeightOfPlatform,
                                                  _game.Window.ClientBounds.Width, HeightOfPlatform));

            //Singleplayer
            _players.Add(new Player(_game, "Figure/lilastoy", new KeyboardMapping(Keys.A, Keys.D, Keys.W), 
                new Vector2(_game.Window.ClientBounds.Width / 2f, _game.Window.ClientBounds.Height)));

            //If multiplayer
            if (_numberOfPlayers > 1)
                _players.Add(new Player(_game, "Figure/greenstoy", new KeyboardMapping(Keys.Left, Keys.Right, Keys.Up), 
                    new Vector2(_game.Window.ClientBounds.Width / 2f, _game.Window.ClientBounds.Height)));

            _camera = new CameraManager(GraphicsDevice.Viewport, CameraVelocity);
            _gameVelocity = _camera.DefaultStartSpeed;

            GeneratePlatforms(_numberOfPlatforms, MinimumDistancePlatform, _maxDistance, _platformWidth);

            _background = _game.Content.Load<Texture2D>(@"Figure\bg");

            //Load score spritefont
            _fonts.Add(new Font(_game, _fontString, Color.White, new Vector2(20, 20)));

            _startGameSong = _game.Content.Load<Song>("Audio/StartGameSong");

            //Background image
            _mainFrame = new Rectangle(0, BackgroundImageYMinimum, GraphicsDevice.Viewport.Width, BackgroundImageYMaximum);
            base.LoadContent();
        }

        public override void Show()
        {
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_startGameSong);
            Initialize();
            LoadContent();
            base.Show();
        }

        public override void Hide()
        {
            MediaPlayer.Stop();
            base.Hide();
        }

        public override void Pause()
        {
            MediaPlayer.Pause();
            base.Pause();
        }

        public override void Resume()
        {
            MediaPlayer.Resume();
            base.Resume();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            _playerPosition = new List<Vector2>();

            if (InputManager.Instance.IsKeyPressed(Keys.Escape))
                PopUp(GameStates.InGameMenu);

            foreach (var player in _players)
            {
                player.Update(gameTime, _game.Window.ClientBounds);
                _playerPosition.Add(player.Position);
                
                foreach (var floor in _platforms)
                {
                    //Check if player hits a platform
                    if (player.CollisionRectangle.Intersects(floor.CollisionRectangle)
                        && player.HitPlatform == false && (player.JumpHeight + player.Texture.Height) < floor.Position.Y)
                    {
                        player.LandedOnPlatForm(floor);
                        _camera.StartCam = true;
                        UpdatePlayerScores();
                    }
                    //Sjekker om spilleren har gått av platformen
                    if (player.HitPlatform && !player.CollisionRectangle.Intersects(floor.CollisionRectangle) && floor == player.Platform)
                        player.WalkedOfPlatform();
                }

                //Check if all players is out of sight
                if (GameOver(_players, _camera.Center))
                {
                    //Add players to score list
                    var scoreList = _players.Select(p => p.Score).ToList();

                    ((GameOver)GetState(GameStates.GameOver)).PrepareGameOverScreen(scoreList);
                    ChangeStateTo(GameStates.GameOver);
                    _camera.StartCam = false;
                }

                //CHeck if player is finished with an level, if, start new level
                if (player.Position.Y < _platforms[_platforms.Count - 1].Position.Y)
                    LevelUp();
            }
            _camera.Update(_playerPosition, gameTime);

            foreach (var font in _fonts)
            {
                font.FontText = _fontString;
                font.Position = new Vector2(font.Position.X, font.Position.Y+_camera.Velocity);
                font.Update(gameTime, _game.Window.ClientBounds);
            }

            foreach (var floor in _platforms)
                floor.Update(gameTime, _game.Window.ClientBounds);

            //Update player score
            ScoreFontText();

            base.Update(gameTime);
        }

        protected void LevelUp()
        {
            //DifficulityFactor is being divided on 2, so the width is not decreasing too fast.
            if (_platformWidth > MinimumPlatformWidth)
                _platformWidth = _platformWidth - DifficulityFactor / 2;

            if (_maxDistance < DistanceBetweenPlatforms)
                _maxDistance = _maxDistance + DifficulityFactor;

            //DifficulityFactor is being divided on 4, so the numberofplatforms isnt increasing too fast
            _numberOfPlatforms = _numberOfPlatforms + DifficulityFactor / 4;

            GeneratePlatforms(_numberOfPlatforms, MinimumDistancePlatform, _maxDistance, _platformWidth);

            if (!(_gameVelocity <= MaximumSpeedLimit)) 
                return;

            _camera.IncreaseSpeed();
            _gameVelocity += _camera.VelocityDelta;
        }

        /// <summary>
        /// Updating the string of the font which displays the players score.
        /// </summary>
        private void ScoreFontText() 
        {
            _fontString = String.Empty;
            var playerCounter = 1;
            foreach(var p in _players){
                _fontString += "Player " + playerCounter + " Score: " + p.Score + Environment.NewLine;
                playerCounter++;
            }
        }

        /// <summary>
        /// Updating the scores of all players in the list _players
        /// </summary>
        private void UpdatePlayerScores()
        {
            var count = 1;
            foreach (var pl in _players)
            {
                foreach (var p in _platforms)
                {
                    if (pl.Platform == p && pl.Score < count)
                        pl.Score = count;
                    count++;
                }
                count = 1;
            }
        }

       /// <summary>
       /// Generating platforms, based on the arguments. The platforms is being added to the list _platforms.
       /// </summary>
       /// <param name="numberOfPlatforms"></param>
       /// <param name="minDistance"></param>
       /// <param name="maxDistance"></param>
       /// <param name="minWidth"></param>
       protected void GeneratePlatforms(int numberOfPlatforms, int minDistance, int maxDistance, int minWidth)
        {
            var count = 0;

            while (count < numberOfPlatforms)
            {
                Platform platform;

                // The distance between the new and the previous platform in x-direction. 
                var x = _random.Next(minDistance, maxDistance);
                
                var width = _random.Next(minWidth, MaximumPlatformWidth);
                //Decides wether the new platform should be added to the left or to the right, therefor random
                var whichDirection = _random.Next(1, 3);

                if (count + 1 == numberOfPlatforms)
                    platform = new Platform(_game, 1,
                       _platforms[_platforms.Count - 1].Position.Y - HeightBetweenPlatforms, 
                       _game.Window.ClientBounds.Width - 1, HeightOfPlatform);
                else if (whichDirection == 1)
                    platform = new Platform(_game, _platforms[_platforms.Count - 1].Position.X - x,
                        _platforms[_platforms.Count - 1].Position.Y - HeightBetweenPlatforms, width, HeightOfPlatform);
                else
                    platform = new Platform(_game, _platforms[_platforms.Count - 1].Position.X + x, 
                        _platforms[_platforms.Count - 1].Position.Y - HeightBetweenPlatforms, width, HeightOfPlatform);

                //Check if platform is out of the screen
                if (CheckOutsideRange(platform)) 
                    continue;
                
                _platforms.Add(platform);
                count++;
            }
        }
        
        /// <summary>
        /// Takes in a platform as argument, to check if its outside the game window.
        /// </summary>
        /// <param name="floor"></param>
        /// <returns></returns>
        private bool CheckOutsideRange(Platform floor)
        {
            return floor.Position.X <= 0 || floor.Position.X + floor.Texture.Width >= _game.Window.ClientBounds.Width;
        }

        /// <summary>
        /// To check wether all players is outside the game window.
        /// If a player is outside, the attribute IsDead is sat to true.
        /// </summary>
        /// <param name="players"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        private bool GameOver(IEnumerable<Player> players, Vector2 center)
        {
            var gameOver = true;
            foreach (var p in players)
                if (!(p.Position.Y > center.Y + _game.Window.ClientBounds.Height / 2f))
                    gameOver = false;
                else
                    p.Dead = true;
            
            return gameOver;
        }

        /// <summary>
        /// Method for drawing
        /// </summary>
        /// <param name="gameTime"></param>
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

            foreach (var floor in _platforms)
                floor.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

