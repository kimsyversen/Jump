using System;
using System.Collections.Generic;
using WindowsGame1WithPatterns.Classes.KeyboardConfiguration;
using WindowsGame1WithPatterns.Classes.Sprites.Factories;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Floors;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1WithPatterns.Classes.States
{
    class Game : State
    {
        private SpriteBatch _spriteBatch;
        private SpriteFactory _spriteFactory;
        private PlayerFactory _playerFactory;
        private FontFactory _fontFactory;
        private FloorFactory _floorFactory;

        private List<IPlayer> _players;
        private List<IFont> _fonts;
        private List<IFloor> _floors;

        public Game(Microsoft.Xna.Framework.Game game, string managerId) : base(game, managerId, GameStates.InGame)
        {
        }
        public override void Initialize()
        {
            _spriteFactory = new SpriteFactory(Game);
            _players = new List<IPlayer>();
            _fonts = new List<IFont>();
            _floors = new List<IFloor>();

            base.Initialize();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _playerFactory = _spriteFactory.CreatePlayerFactory();

            _floorFactory = _spriteFactory.CreateFloorFactory();

            var floor = _floorFactory.CreateFloorSprite();
            var floor2 = _floorFactory.CreateFloorSprite1();
            var floor3 = _floorFactory.CreateFloorSprite2();
            _floors.Add(floor);
            _floors.Add(floor2);
            _floors.Add(floor3);

            var player = _playerFactory.CreatePlayerOne();
            //var player2 = _playerFactory.CreatePlayerTwo();

            _players.Add(player);
            // _players.Add(player2);
            _fontFactory = _spriteFactory.CreateFontFactory();

            var font = _fontFactory.PlayerScoreFont(player);

            _fonts.Add(font);

            base.LoadContent();
        }

        private int teller = 0;

        public override void Update(GameTime gameTime)
        {
            if (KeyboardManager.IsKeyDown(Keys.Escape))
                ChangeStateTo(GameStates.MainMenu);

            if (KeyboardManager.IsKeyDown(Keys.P))
                ChangeStateTo(GameStates.GameOver);

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Game.Exit();

            foreach (var player in _players)
            {
                player.Update(gameTime, Game.Window.ClientBounds);

                foreach (var floor in _floors)
                {

                    if (player.Collide.Intersects(floor.Collide) && player.HasHitPlatform == false && (player.GetY + player.PlayerTexture.Height) < floor.FloorPosition.Y)
                    {
                        //Må passe på at spilleren blir tegnet på toppen av platformen
                        player.PlayerPosition = new Vector2(player.PlayerPosition.X, (floor.FloorPosition.Y - player.PlayerTexture.Height + 1));

                        Console.WriteLine(floor.ToString());
                        player.HasJumped = false;
                        player.HasHitTheWall = false;
                        player.HasHitPlatform = true;
                        player.GetY = Game.Window.ClientBounds.Height;
                        player.OnFloor = floor;
                    }
                    //Sjekker om spilleren hat gått av platformen
                    if (player.HasHitPlatform && !player.Collide.Intersects(floor.Collide) && floor == player.OnFloor)
                    {
                        player.HasHitPlatform = false;
                        player.HasJumped = true;
                    }
                }
            }
            if (teller == 1)
            {
                _floors.Add(_floorFactory.CreateFloorSpriteInputs(50, 50, 100, 5));
                teller++;
            }
            foreach (var font in _fonts)
                font.Update(gameTime, Game.Window.ClientBounds);

            foreach (var floor in _floors)
                floor.Update(gameTime, Game.Window.ClientBounds);

            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

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
