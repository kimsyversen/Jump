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
    internal class PlayerNotFontSprite : NotFontSprite, IPlayer
    {
        private Game _game;
        private readonly List<IFont> _observers;
        private bool _hasJumped;
        private bool _hasHitTheWall;
        private readonly bool _player;
        private bool _platformHit = false;
        private IFloor _platform;
        private float y;
        private Keys left;
        private Keys right;
        private Keys up;

        //private KeyController _keyController;
        public PlayerNotFontSprite(Game game, bool newPlayer)
            : this(game.Content.Load<Texture2D>(@"Ball"),
                new Vector2(game.Window.ClientBounds.Width / 2f, game.Window.ClientBounds.Height / 2f), new Point(30, 30), new Point(0, 0),
                new Point(0, 0), 0f, Vector2.Zero, 1f, SpriteEffects.None, new Vector2(0, 0), 0, 100)
        {
            _game = game;
            _observers = new List<IFont>();
            _hasJumped = true;
            _hasHitTheWall = false;
            _player = newPlayer;
           // _keyController = new KeyController(Keys.A, Keys.D, Keys.Space);


            if (_player)
            {
                left = Keys.A;
                right = Keys.D;
                up = Keys.W;
            }
            else
            {
                left = Keys.Left;
                right = Keys.Right;
                up = Keys.Up;
            }
        }

        public PlayerNotFontSprite(Texture2D texture, Vector2 spritePosition, Point frameSize, Point frameCurrent,
                            Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects,
                            Vector2 speed, int collisionOffset, int timeSinceLastFrame)
            : base(
                texture, spritePosition, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, speed,
                collisionOffset, timeSinceLastFrame)
        {
        }

        public PlayerNotFontSprite(Texture2D texture, Vector2 spritePosition, Point frameSize, Point frameCurrent,
                            Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects,
                            Vector2 speed, int collisionOffset, int millisecondsPerFrame, int timeSinceLastFrame)
            : base(
                texture, spritePosition, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, speed,
                collisionOffset, millisecondsPerFrame, timeSinceLastFrame)
        {
        }
        const float playerSpeed = 3.0f;


        public new void Update(GameTime gameTime, Rectangle clientBounds)
        {


            if (Keyboard.GetState().IsKeyDown(right) && _hasHitTheWall == false) Speed.X = playerSpeed;
            else if (Keyboard.GetState().IsKeyDown(left) && _hasHitTheWall == false) Speed.X = -playerSpeed;
            else if (_hasHitTheWall == false) Speed.X = 0f;

            if (Keyboard.GetState().IsKeyDown(up) && _hasJumped == false)
            {
                SpritePosition.Y -= 10f;
                Speed.Y = -20f;
                _hasJumped = true;
            }

            if (_hasJumped)
            {
                float i = 1;
                Speed.Y += 0.15f + i;
                if (SpritePosition.Y < y) y = SpritePosition.Y;
            }
            if (SpritePosition.Y + Texture.Height >= clientBounds.Height)
            {
                y = clientBounds.Height;
                _hasJumped = false;
                _hasHitTheWall = false;
                SpritePosition.Y = clientBounds.Height - Texture.Height;
            }

            if (_hasJumped == false)
            {
                Speed.Y = 0f;
            }


            if (SpritePosition.X <= 0)
            {
                if (_hasJumped == true)
                {
                    Speed.X = playerSpeed;
                    Speed.Y = -playerSpeed - 5;
                    _hasHitTheWall = true;
                }
                else SpritePosition.X = 0;

            }
            if (SpritePosition.X >= (clientBounds.Width - Texture.Width))
            {
                if (_hasJumped == true)
                {
                    Speed.X = -playerSpeed;
                    Speed.Y = -playerSpeed - 5;
                    _hasHitTheWall = true;
                }
                else SpritePosition.X = clientBounds.Width - Texture.Width;
            }

            //Bruker MoveCommand for flyttingen, og gir beskjed til observer
            var cmd = new MoveCommand(this, new Vector2(Speed.X, Speed.Y), new Vector2(SpritePosition.X + Speed.X, SpritePosition.Y + Speed.Y));
            cmd.Execute();
            NotifyObservers();

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
            get { return Speed; }
            set
            {
                Speed = value;
            }
        }

        public Vector2 PlayerPosition
        {
            get { return SpritePosition; }
            set { SpritePosition = value; }
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
        public float GetY { get { Console.Write(y); return y; } set { y = value; } }
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
                observer.UpdateCoordinates(this.SpritePosition);
        }

        #endregion
    }
}
