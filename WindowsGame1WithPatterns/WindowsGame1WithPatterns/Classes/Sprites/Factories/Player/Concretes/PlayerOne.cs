using System.Collections.Generic;

using WindowsGame1WithPatterns.Classes.Sprites.Factories.Floors;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player.Movement;
using System;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Player.Concretes
{
    internal class PlayerOne : NotFontSprite, IPlayer
    {
        private Game _game;
        private readonly List<IFont> _observers;
        private bool _hasJumped;
        private bool _hasHitTheWall;
        private readonly bool _player;
        private bool _platformHit = false;
        private IFloor _platform;
        private float WhatIsDis;
        private KeyboardConfiguration.KeyboardConfiguration _keyboardConfiguration;


        private float moveSpeed = 4f;


        public PlayerOne(Game game, bool newPlayer)
            : this(game.Content.Load<Texture2D>(@"Ball"),
                new Vector2(game.Window.ClientBounds.Width / 2f, game.Window.ClientBounds.Height ), new Point(30, 30), new Point(0, 0),
                new Point(0, 0), 0f, Vector2.Zero, 1f, SpriteEffects.None, new Vector2(10, 10), 0, 100)
        {
            _game = game;
            _observers = new List<IFont>();
            _hasJumped = true;
            _hasHitTheWall = false;
            _player = newPlayer;
            _keyboardConfiguration = new KeyboardConfiguration.KeyboardConfiguration(Keys.A, Keys.None, Keys.D, Keys.None, Keys.Escape, Keys.Enter, Keys.Space);

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
            if (Keyboard.GetState().IsKeyDown(_keyboardConfiguration.Right) && _hasHitTheWall == false) Velocity.X = moveSpeed;
            else if (Keyboard.GetState().IsKeyDown(_keyboardConfiguration.Left) && _hasHitTheWall == false) Velocity.X = -moveSpeed;
            else if (_hasHitTheWall == false) Velocity.X = 0f;

            if (Keyboard.GetState().IsKeyDown(_keyboardConfiguration.Jump) && _hasJumped == false)
            {
                Position.Y -= 10f;
                Velocity.Y = -20f;
                _hasJumped = true;
            }

            if (_hasJumped)
            {
                float i = 1;
                Velocity.Y += 0.15f + i;
                if (Position.Y < WhatIsDis) WhatIsDis = Position.Y;
            }
            if (Position.Y + Texture.Height >= clientBounds.Height)
            {
                WhatIsDis = clientBounds.Height;
                _hasJumped = false;
                _hasHitTheWall = false;
                Position.Y = clientBounds.Height - Texture.Height;
            }

            if (_hasJumped == false)
            {
                Velocity.Y = 0f;
            }


            if (Position.X <= 0)
            {
                if (_hasJumped == true)
                {
                    Velocity.X = moveSpeed;
                    Velocity.Y = -moveSpeed - 5;
                    _hasHitTheWall = true;
                }
                else Position.X = 0;

            }
            if (Position.X >= (clientBounds.Width - Texture.Width))
            {
                if (_hasJumped == true)
                {
                    Velocity.X = -moveSpeed;
                    Velocity.Y = -moveSpeed - 5;
                    _hasHitTheWall = true;
                }
                else Position.X = clientBounds.Width - Texture.Width;
            }

            //Bruker MoveCommand for flyttingen, og gir beskjed til observer
            var cmd = new MoveCommand(this, new Vector2(Velocity.X, Velocity.Y), new Vector2(Position.X + Velocity.X, Position.Y + Velocity.Y));
            cmd.Execute();
            NotifyObservers();

            //Animate sprite
            base.Update(gameTime, clientBounds);
        }

        public new void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }


        public Vector2 PlayerSpeed
        {
            get { return Velocity; }
            set { Velocity = value; }
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

        public IFloor OnFloor
        {
            get { return _platform; }
            set { _platform = value; }
        }

        public Rectangle Collide { get { return CollisionRectangle; } }
        public float GetY { get { Console.Write(WhatIsDis); return WhatIsDis; } set { WhatIsDis = value; } }
        public Texture2D PlayerTexture { get { return this.Texture; } }

        #region ObserverPatternRelated

        public void RegisterObserver(IFont observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IFont observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
                observer.UpdateCoordinates(this.Position);
        }

        #endregion
    }
}
