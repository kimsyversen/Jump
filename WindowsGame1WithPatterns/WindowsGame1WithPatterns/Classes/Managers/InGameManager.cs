﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WindowsGame1WithPatterns.Classes.KeyboardConfiguration;
using WindowsGame1WithPatterns.Classes.Sprites.Factories;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Floors;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player;
using WindowsGame1WithPatterns.Classes.CameraConfiguration;

namespace WindowsGame1WithPatterns.Classes.Managers
{
    //TODO: RYDDE OPP.
    class InGameManager : DrawableGameComponent, IManager
    {
        SpriteBatch _spriteBatch;
        CameraManager camera;
        private SpriteFactory _spriteFactory;
        private PlayerFactory _playerFactory;
        private FontFactory _fontFactory;
        private FloorFactory _floorFactory;

        private List<IPlayer> _players;
        private List<IFont> _fonts;
        private List<IFloor> _floors;

        private Manager _manager;
        
        public InGameManager(Game game, Manager manager) : base(game)
        {
            _manager = manager;
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
            var floor4 = _floorFactory.CreateFloorSprite3();
            var floor5 = _floorFactory.CreateFloorSprite4();
            var floor6 = _floorFactory.CreateFloorSprite5();
            var floor7 = _floorFactory.CreateFloorSprite6();
            var floor8 = _floorFactory.CreateFloorSprite7();
            var floor9 = _floorFactory.CreateFloorSprite8();
            _floors.Add(floor);
            _floors.Add(floor2);
            _floors.Add(floor3);
            _floors.Add(floor4);
            _floors.Add(floor5);
            _floors.Add(floor6);
            _floors.Add(floor7);
            _floors.Add(floor8); 
            _floors.Add(floor9);

            var player = _playerFactory.CreatePlayerOne();
            //var player2 = _playerFactory.CreatePlayerTwo();

            camera = new CameraManager(GraphicsDevice.Viewport);

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
                _manager.InMenu();

            if (KeyboardManager.IsKeyDown(Keys.P))
                _manager.InGameOver();

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
            //Camera inputs: player position, xOffset, yOffset
            camera.Update(_players[0].PlayerPosition, 557, 1100);

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

            _spriteBatch.Begin(SpriteSortMode.Deferred,
                        BlendState.AlphaBlend,
                        null, null, null, null,
                        camera.Transform);

            foreach (var font in _fonts)
                font.Draw(gameTime, _spriteBatch);

            foreach (var floor in _floors)
                floor.Draw(gameTime, _spriteBatch);

            foreach (var player in _players)
                player.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        public void Enable(bool value)
        {
            Visible = value;
            Enabled = value;
        }
    }

}
