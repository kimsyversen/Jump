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
    internal class PlayerTwo : NotFontSprite, IPlayer
    {
        private Game _game;
        private readonly List<IFont> _observers;
        private bool _hasJumped;
        private bool _hasHitTheWall;
        private readonly bool _player;
        private bool _platformHit = false;
        private IPlatform _platform;
        private float y;
        private KeyController _keyController;

        public PlayerTwo(Game game, bool newPlayer)
            : this(game.Content.Load<Texture2D>(@"Ball"),
                new Vector2(game.Window.ClientBounds.Width / 2f, game.Window.ClientBounds.Height / 2f), new Point(30, 30), new Point(0, 0),
                new Point(0, 0), 0f, Vector2.Zero, 1f, SpriteEffects.None, new Vector2(0, 0), 0, 100)
        {
            _game = game;
            _observers = new List<IFont>();
            _hasJumped = true;
            _hasHitTheWall = false;
            _player = newPlayer;
            _keyController = new KeyController(Keys.Left, Keys.Right, Keys.Up);
        }

        public PlayerTwo(Texture2D texture, Vector2 position, Point frameSize, Point frameCurrent,
                            Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects,
                            Vector2 velocity, int collisionOffset, int timeSinceLastFrame)
            : base(
                texture, position, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, velocity,
                collisionOffset, timeSinceLastFrame)
        {
        }

        public PlayerTwo(Texture2D texture, Vector2 position, Point frameSize, Point frameCurrent,
                            Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects,
                            Vector2 velocity, int collisionOffset, int millisecondsPerFrame, int timeSinceLastFrame)
            : base(
                texture, position, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, velocity,
                collisionOffset, millisecondsPerFrame, timeSinceLastFrame)
        {
        }
        const float playerSpeed = 3.0f;


        public new void Update(GameTime gameTime, Rectangle clientBounds)
        {
            if (Keyboard.GetState().IsKeyDown(_keyController.Right) && _hasHitTheWall == false) Velocity.X = playerSpeed;
            else if (Keyboard.GetState().IsKeyDown(_keyController.Left) && _hasHitTheWall == false) Velocity.X = -playerSpeed;
            else if (_hasHitTheWall == false) Velocity.X = 0f;

            if (Keyboard.GetState().IsKeyDown(_keyController.Jump) && _hasJumped == false)
            {
                Position.Y -= 10f;
                Velocity.Y = -20f;
                _hasJumped = true;
            }

            if (_hasJumped)
            {
                float i = 1;
                Velocity.Y += 0.15f + i;
                if (Position.Y < y) y = Position.Y;
            }
            if (Position.Y + Texture.Height >= clientBounds.Height)
            {
                y = clientBounds.Height;
                _hasJumped = false;
                _hasHitTheWall = false;
                Position.Y = clientBounds.Height - Texture.Height;
            }

            if (_hasJumped == false)
                Velocity.Y = 0f;
  
            if (Position.X <= 0)
            {
                if (_hasJumped)
                {
                    Velocity.X = playerSpeed;
                    Velocity.Y = -playerSpeed - 5;
                    _hasHitTheWall = true;
                }
                else Position.X = 0;

            }
            if (Position.X >= (clientBounds.Width - Texture.Width))
            {
                if (_hasJumped)
                {
                    Velocity.X = -playerSpeed;
                    Velocity.Y = -playerSpeed - 5;
                    _hasHitTheWall = true;
                }
                else Position.X = clientBounds.Width - Texture.Width;
            }

            //Bruker MoveCommand for flyttingen, og gir beskjed til observer
            var cmd = new MoveCommand(this, new Vector2(Velocity.X, Velocity.Y), new Vector2(Position.X + Velocity.X, Position.Y + Velocity.Y));
            cmd.Execute();
            //kan gjøres i movecommand?
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
        public float GetY { get { Console.Write(y); return y; } set { y = value; } }
        public Texture2D PlayerTexture { get { return this.Texture; } }

        public IPlatform PlayerOnPlatform
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        #region ObserverPatternRelated

        public void RegisterFontObserver(IFont observer)
        {
            _observers.Add(observer);
        }

        public void RemoveFontObserver(IFont observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyFontObservers()
        {
            foreach (var observer in _observers)
                observer.UpdateCoordinates(this.Position);
        }

        public void RegisterPlatformObserver(IPlatform observer)
        {
            throw new NotImplementedException();
        }

        public void RemovePlatformObserver(IPlatform observer)
        {
            throw new NotImplementedException();
        }

        public void NotifyPlatformObservers()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
