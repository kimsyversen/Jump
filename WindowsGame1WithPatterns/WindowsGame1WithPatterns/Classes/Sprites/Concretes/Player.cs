using System.Collections.Generic;
using WindowsGame1WithPatterns.Classes.KeyboardConfiguration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using WindowsGame1WithPatterns.Classes.Sprites.Concretes.Movement;

namespace WindowsGame1WithPatterns.Classes.Sprites.Concretes
{
    internal class Player : NotFontSprite
    {
        private Game _game;

        private bool _hasJumped;
        private bool _hasHitTheWall;
        private bool _platformHit = false;
        private Platform _platform;
        private float _heightOfJump;
        private const float Gravity = 1.15f;
        private const float JumpHeight = 10f;

        const float playerSpeed = 3.0f;
        const float playerSpeedChange = 5.0f;

        private SoundEffect effect;

        /// <summary>
        /// Used to assign keys to players
        /// </summary>
        private KeyboardMapping _keyboardMapping;


        public Player(Game game, String filnavn, KeyboardMapping keyboardMapping)
            : this(game.Content.Load<Texture2D>(filnavn),
                new Vector2(game.Window.ClientBounds.Width / 2f, game.Window.ClientBounds.Height - 48), new Point(48, 48), new Point(0, 0),
                new Point(0, 0), 0f, Vector2.Zero, 1f, SpriteEffects.None, new Vector2(0, 0), 0, 100)
        {
            _game = game;

            _hasJumped = true;
            _hasHitTheWall = false;
            effect = game.Content.Load<SoundEffect>("Audio/Jump");
            _keyboardMapping = keyboardMapping;


        }

        public Player(Texture2D texture, Vector2 spritePosition, Point frameSize, Point frameCurrent,
                            Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects,
                            Vector2 speed, int collisionOffset, int timeSinceLastFrame)
            : base(
                texture, spritePosition, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, speed,
                collisionOffset, timeSinceLastFrame)
        {
        }

        public Player(Texture2D texture, Vector2 spritePosition, Point frameSize, Point frameCurrent,
                            Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects,
                            Vector2 speed, int collisionOffset, int millisecondsPerFrame, int timeSinceLastFrame)
            : base(
                texture, spritePosition, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, speed,
                collisionOffset, millisecondsPerFrame, timeSinceLastFrame)
        {
        }
     


        public new void Update(GameTime gameTime, Rectangle clientBounds)
        {
            if (InputManager.Instance.IsKeyDown(_keyboardMapping.Right) && !_hasHitTheWall) 
                Velocity.X = playerSpeed;
            else if (InputManager.Instance.IsKeyDown(_keyboardMapping.Left) && !_hasHitTheWall) 
                Velocity.X = -playerSpeed;
            else if (!_hasHitTheWall)
                Velocity.X = 0f;

            if (InputManager.Instance.IsKeyDown(_keyboardMapping.Jump) && !_hasJumped)
            {
                Position.Y -= JumpHeight;
                Velocity.Y = -20f;
                _hasJumped = true;
                effect.Play();
            }

            if (_hasJumped)
            {
                Velocity.Y += Gravity;
                if (Position.Y < _heightOfJump) _heightOfJump = Position.Y;
            }

            if (Position.Y + Texture.Height >= clientBounds.Height)
            {
                _heightOfJump = clientBounds.Height;
                _hasJumped = false;
                _hasHitTheWall = false;
                Position.Y = clientBounds.Height - Texture.Height;
            }

            if (!_hasJumped)
                Velocity.Y = 0f;
            
            if (Position.X <= 0)
                if (_hasJumped)
                {
                    Velocity.X = playerSpeed;
                    Velocity.Y = -playerSpeed - playerSpeedChange;
                    _hasHitTheWall = true;
                }
                else 
                    Position.X = 0;

            
            if (Position.X >= (clientBounds.Width - Texture.Width))
                if (_hasJumped)
                {
                    Velocity.X = -playerSpeed;
                    Velocity.Y = -playerSpeed - playerSpeedChange;
                    _hasHitTheWall = true;
                }
                else 
                    Position.X = clientBounds.Width - Texture.Width;
            

            //Bruker MoveCommand for flyttingen
            var cmd = new MoveCommand(this, new Vector2(Velocity.X, Velocity.Y), new Vector2(Position.X + Velocity.X, Position.Y + Velocity.Y));
            cmd.Execute();
        

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

        public void LandedOnPlatForm(Platform floor)
        {
            //Må passe på at spilleren blir tegnet på toppen av platformen
            var newPosition = new Vector2(PlayerPosition.X, (floor.FloorPosition.Y - Texture.Height + 1));
            PlayerPosition = newPosition;

            _hasJumped = false;
            _hasHitTheWall = false;
            _platformHit = true;
            _heightOfJump = floor.FloorPosition.Y;
            _platform = floor;
        }

        public void WalkedOfPlatform()
        {
            _platformHit = false;
            _hasJumped = true;
        }
        public bool HasHitPlatform
        {
            get { return _platformHit; }
        }

        public Platform OnFloor
        {
            get { return _platform; }
        }

        public Rectangle Collide { get { return CollisionRectangle; } }
        public float GetY { get { return _heightOfJump; } }
        public Texture2D PlayerTexture { get { return Texture; } }

    }
}
