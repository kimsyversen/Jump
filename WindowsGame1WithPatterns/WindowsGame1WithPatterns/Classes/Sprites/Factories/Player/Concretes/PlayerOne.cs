using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Platform;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player.Movement;
using System;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Player.Concretes
{
    internal class PlayerOne : NotFontSprite, IPlayer
    {
        private Game _game;
        private readonly List<IFont> _fontObservers;
        private IPlatform _platformPlayerIsOn;

        private bool _hasJumped;
        private bool _hasHitTheWall;
        private readonly bool _player;
        private bool _platformHit = false;
        private IPlatform _platform;
        private float WhatIsDis;
        private KeyController _keyController;

  
        const float gravity = 3f;

        private float moveSpeed = 4f;
        private float jumpSpeed = 250f;
        private bool _canJump = false;



        public PlayerOne(Game game, bool newPlayer)
            : this(game.Content.Load<Texture2D>(@"Ball"),
                new Vector2(game.Window.ClientBounds.Width / 2f, game.Window.ClientBounds.Height ), new Point(30, 30), new Point(0, 0),
                new Point(0, 0), 0f, Vector2.Zero, 1f, SpriteEffects.None, new Vector2(10, 10), 0, 100)
        {
            _game = game;
            _fontObservers = new List<IFont>();
            _hasJumped = true;
            _hasHitTheWall = false;
            _player = newPlayer;
            _keyController = new KeyController(Keys.A, Keys.D, Keys.Space);

        }

        public PlayerOne(Texture2D texture, Vector2 position, Point frameSize, Point frameCurrent,
                            Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects,
                            Vector2 velocity, int collisionOffset, int timeSinceLastFrame)
            : base(
                texture, position, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, velocity,
                collisionOffset, timeSinceLastFrame)
        {
        }

        public PlayerOne(Texture2D texture, Vector2 position, Point frameSize, Point frameCurrent,
                            Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects,
                            Vector2 velocity, int collisionOffset, int millisecondsPerFrame, int timeSinceLastFrame)
            : base(
                texture, position, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, velocity,
                collisionOffset, millisecondsPerFrame, timeSinceLastFrame)
        {
        }


        public new void Update(GameTime gameTime, Rectangle clientBounds)
        {
            //Outside Y bottom
            if (Position.Y + Texture.Height >= clientBounds.Height)
            {
                Position.Y = clientBounds.Height - Texture.Height;
                Velocity.Y = 0;
                _canJump = true;
            }
                
            //Outside Y top
            if (Position.Y + Texture.Height <= 0)
                Position.Y = clientBounds.Height - Texture.Height;

            //Outside X to right
            if (Position.X >= (clientBounds.Width - Texture.Width))
                Position.X = clientBounds.Width - Texture.Width;

            //Outside X to left
            if (Position.X <= clientBounds.Width - clientBounds.Width)
                Position.X = clientBounds.Width - clientBounds.Width;

            //Increase velocity if player goes left or right. If none, velocity is zero 
            if (Keyboard.GetState().IsKeyDown(_keyController.Right))
                Velocity.X += moveSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;
            else if (Keyboard.GetState().IsKeyDown(_keyController.Left))
                Velocity.X -= moveSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;
            else
                Velocity.X = 0;

            //if player jumps, change velocity
            if (Keyboard.GetState().IsKeyDown(_keyController.Jump) && _canJump)
            {
                Velocity.Y = -jumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _canJump = false;
                HasHitPlatform = false;
            }
            
            //If player is in air, let the player fall down because of gravity
            Velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!_canJump)
                Velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
                Velocity.Y = 0f;

            //Position increases with velocity over time
            Position += Velocity;



            /*
                        if (Position.Y + Texture.Height >= clientBounds.Height)
                        {
                            WhatIsDis = clientBounds.Height;
                            _hasJumped = false;
                            _hasHitTheWall = false;
                            Position.Y = clientBounds.Height - Texture.Height;
                        }

                        if (Position.X <= (clientBounds.Width - clientBounds.Width))
                        {
                            if (_hasJumped)
                            {
                                Velocity.X = DefaultPlayerSpeed;
                                // Velocity.Y = -_defaultPlayerSpeed - 5;
                                _hasHitTheWall = true;
                            }
                            else Position.X = 0;

                        }
                        if (Position.X >= (clientBounds.Width - Texture.Width))
                        {
                            if (_hasJumped)
                            {
                                Velocity.X = -DefaultPlayerSpeed;
                                //Velocity.Y = -_defaultPlayerSpeed - 5;
                                _hasHitTheWall = true;
                            }
                            else
                                Position.X = clientBounds.Width - Texture.Width;
                        }


                        
                        if (Keyboard.GetState().IsKeyDown(_keyController.Jump) && _hasJumped == false)
                        {
                            Velocity = new Vector2(Velocity.X, Velocity.Y -20);
                            Position.Y -= 10f;
                            Position = new Vector2(Position.X, Position.Y - 10f);
                            _hasJumped = true;
                        }

                        if (_hasJumped)
                        {
                            Velocity.Y += _defaultPlayerSpeedIncrease;
                            if (Position.Y < WhatIsDis) 
                                WhatIsDis = Position.Y;
                        }

                        //Make sure sprite dont fall down into eternity
                        if (Position.Y + Texture.Height >= clientBounds.Height)
                        {
                            WhatIsDis = clientBounds.Height;
                            _hasJumped = false;
                            _hasHitTheWall = false;
                            Position.Y = clientBounds.Height - Texture.Height;
                        }

                        if (_hasJumped == false)
                            Velocity.Y = 0f;

                        if (Position.X <= (clientBounds.Width - clientBounds.Width))
                        {
                            if (_hasJumped)
                            {
                                Velocity.X = _defaultPlayerSpeed;
                               // Velocity.Y = -_defaultPlayerSpeed - 5;
                                _hasHitTheWall = true;
                            }
                            else Position.X = 0;

                        }
                        if (Position.X >= (clientBounds.Width - Texture.Width))
                        {
                            if (_hasJumped)
                            {
                                Velocity.X = -_defaultPlayerSpeed;
                                //Velocity.Y = -_defaultPlayerSpeed - 5;
                                _hasHitTheWall = true;
                            }
                            else 
                                Position.X = clientBounds.Width - Texture.Width;
                        }*/

            //Bruker MoveCommand for flyttingen, og gir beskjed til observer
            var cmd = new MoveCommand(this, new Vector2(Velocity.X, Velocity.Y), new Vector2(Position.X + Velocity.X, Position.Y + Velocity.Y));
            cmd.Execute();
            NotifyFontObservers();

            //Animate sprite
            base.Update(gameTime, clientBounds);
        }

        public new void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Let the sprite class do it
            base.Draw(gameTime, spriteBatch);
        }

        public Vector2 PlayerSpeed
        {
            get { return Velocity; }
            set
            {
                Velocity = value;
            }
        }

        public Vector2 PlayerPosition
        {
            get { return Position; }
            set { Position = value; }
        }

        public bool HasJumped
        {
            get { return _hasJumped; }
            set { _hasJumped = value; }
        }

        public bool HasHitTheWall
        {
            get { return _hasHitTheWall; }
            set { _hasHitTheWall = value; }
        }

        public bool HasHitPlatform
        {
            get { return _platformHit; }
            set { _platformHit = value; }
        }

        public IPlatform OnPlatform
        {
            get { return _platform; }
            set { _platform = value; }
        }

        public Rectangle DetectCollision { get { return CollisionRectangle; } }
        public float GetY { get { Console.Write(WhatIsDis); return WhatIsDis; } set { WhatIsDis = value; } }
        public Texture2D PlayerTexture { get { return this.Texture; } }

        public IPlatform PlayerOnPlatform
        {
            get { return _platformPlayerIsOn; }
            set { _platformPlayerIsOn = value; }
        }

        #region ObserverPatternRelated

        public void RegisterFontObserver(IFont observer)
        {
            _fontObservers.Add(observer);
        }

        public void RemoveFontObserver(IFont observer)
        {
            _fontObservers.Remove(observer);
        }

        public void NotifyFontObservers()
        {
            foreach (var observer in _fontObservers)
                observer.UpdateCoordinates(this.Position);
        }


        #endregion
    }
}
