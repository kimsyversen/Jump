using System;
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
        private List<Platform> _platforms;

        private String _fontString = "Initially empty";

        //For platform generation and camera following
        private int _heightOfBoard;

        private int _platformWidth;

        private int _maxDistance;

        private int _numberOfPlatforms;

        private const int MinDistance = 20;
        private const int MinimumPlatformWidth = 10;
        private const int HeightBetweenPlatforms = 70;
        private const int DistanceBetweenPlatforms = 225;
        private const int HeightOfPlatform = 5;
        private const int DifficulityFactor = 20;
        private const float MaxSpeedLimit = 5.0f;
        private const int BgImageYMin = -100000;
        private const int BgImageYMax = 110000;


        private float _gameVelocity;
        private readonly int _numberOfPlayers;

        private Random _rnd;

        private Song _startGameSong;

        protected AbstractGameManager(Game game, SpriteBatch spriteBatch, string managerId, GameStates stateId, GraphicsDeviceManager graphics, int numberOfPlayers)
            : base(game, spriteBatch, managerId, stateId)
        {
           
            _numberOfPlayers = numberOfPlayers;
        }

        public override void Initialize()
        {
            _players = new List<Player>();
            _fonts = new List<Font>();
            _platforms = new List<Platform>();
            _playerPosition = new List<Vector2>();
            _rnd = new Random();
            
            _heightOfBoard = 0;
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
            _players.Add(new Player(_game, "Figure/lilastoy", new KeyboardMapping(Keys.A, Keys.D, Keys.Space), 
                new Vector2(_game.Window.ClientBounds.Width / 2f, _game.Window.ClientBounds.Height)));

            //If multiplayer
            if (_numberOfPlayers > 1)
                _players.Add(new Player(_game, "Figure/greenstoy", new KeyboardMapping(Keys.Left, Keys.Right, Keys.Up), 
                    new Vector2(_game.Window.ClientBounds.Width / 2f, _game.Window.ClientBounds.Height)));

            _camera = new CameraManager(GraphicsDevice.Viewport, -0.1f);
            _gameVelocity = _camera.DefaultStartSpeed;

            GeneratePlatforms(_numberOfPlatforms, MinDistance, _maxDistance, _platformWidth);

            _background = _game.Content.Load<Texture2D>(@"Figure\bg");

            //Load score spritefont
            _fonts.Add(new Font(_game, _fontString, Color.White, new Vector2(20, 20)));
            _startGameSong = _game.Content.Load<Song>("Audio/StartGameSong");


            //Background image
            _mainFrame = new Rectangle(0, BgImageYMin, GraphicsDevice.Viewport.Width, BgImageYMax);
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
                
                foreach (var floor in _platforms)
                {
                    //Check if player hits a platform
                    if (player.CollisionRectangle.Intersects(floor.CollisionRectangle) 
                        && player.HasHitPlatform == false && (player.GetY + player.Texture.Height) < floor.Position.Y)
                    {
                        player.LandedOnPlatForm(floor);
                        _camera.StartCam = true;
                        UpdateScores();
                    }
                    //Sjekker om spilleren har gått av platformen
                    if (player.HasHitPlatform && !player.CollisionRectangle.Intersects(floor.CollisionRectangle) && floor == player.OnFloor)
                        player.WalkedOfPlatform();
                }

                //Check if all players is out of sight
                if (GameOver(_players, _camera.Center))
                {
                    ChangeStateTo(GameStates.GameOver);
                    _camera.StartCam = false;
                    Console.WriteLine(player.Score+ " ");
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

            GeneratePlatforms(_numberOfPlatforms, MinDistance, _maxDistance, _platformWidth);

            if (_gameVelocity <= MaxSpeedLimit)
            {
                _camera.IncreaseSpeed();
                _gameVelocity += _camera.VelocityDelta;
            }
        }

        /// <summary>
        /// Updating the string of the font which displays the players score.
        /// </summary>
        private void ScoreFontText() {
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
        private void UpdateScores()
        {
            var count = 1;
            foreach (var pl in _players)
            {
                foreach (var p in _platforms)
                {
                    if (pl.OnFloor == p && pl.Score < count)
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
                var x = _rnd.Next(minDistance, maxDistance);
                
                var width = _rnd.Next(minWidth, 100);
                //Decides wether the new platform should be added to the left or to the right, therefor random
                var whichDirection = _rnd.Next(1, 3);

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
            _heightOfBoard = HeightBetweenPlatforms * _platforms.Count;
        }
        
        /// <summary>
        /// Takes in a platform as argument, to check if its outside the game window.
        /// </summary>
        /// <param name="floor"></param>
        /// <returns></returns>
        private bool CheckOutsideRange(Platform floor)
        {
            if (floor.Position.X <= 0 || floor.Position.X + floor.Texture.Width >= _game.Window.ClientBounds.Width)
                return true;
            return false;
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
                    p.IsDead = true;
            
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

