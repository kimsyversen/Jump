using System.Collections.Generic;
using WindowsGame1WithPatterns.Classes.KeyboardConfiguration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using WindowsGame1WithPatterns.Classes.Sprites.Concretes;

namespace WindowsGame1WithPatterns.Classes.Sprites.Concretes
{
    internal class Player : Sprite
    {
        private bool _hasJumped;
        private bool _hasHitTheWall;

        public Platform Platform { get; set; }
        private float _heightOfJump;
        private const float Gravity = 1.15f;
        private const float JumpHeight = 10f;
        public bool Dead = false;

        const float PlayerSpeed = 5.0f;
        const float PlayerSpeedChange = 5.0f;
        private readonly SoundEffect _effect;

        public int Score { get; set; }

        public bool HasHitPlatform { get; set; }

        /// <summary>
        /// Used to assign keys to players
        /// </summary>
        private readonly KeyboardMapping _keyboardMapping;


        public Player(Game game, String filnavn, KeyboardMapping keyboardMapping, Vector2 position)
            : this(game, game.Content.Load<Texture2D>(filnavn),
                new Vector2(game.Window.ClientBounds.Width / 2f, game.Window.ClientBounds.Height - 48), new Point(48, 48), new Point(0, 0),
                new Point(0, 0), 0f, Vector2.Zero, 1f, SpriteEffects.None, new Vector2(0, 0), 0, 100)
        {

            Dead = false;
            _hasJumped = true;
            _hasHitTheWall = false;
            _effect = game.Content.Load<SoundEffect>("Audio/Jump");
            Position = position;
            _keyboardMapping = keyboardMapping;
        }

        public Player(Game game, Texture2D texture, Vector2 spritePosition, Point frameSize, Point frameCurrent,
                            Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects,
                            Vector2 speed, int collisionOffset, int timeSinceLastFrame)
            : base(game,
                texture, spritePosition, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, speed,
                collisionOffset, timeSinceLastFrame)
        {
        }

        public Player(Game game, Texture2D texture, Vector2 spritePosition, Point frameSize, Point frameCurrent,
                            Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects,
                            Vector2 speed, int collisionOffset, int millisecondsPerFrame, int timeSinceLastFrame)
            : base(game,
                texture, spritePosition, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, speed,
                collisionOffset, millisecondsPerFrame, timeSinceLastFrame)
        {
        }
     
        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            if (Dead)
            {
                velocity.Y = 0f;
                velocity.X = 0f;
                return;
            }
            if (InputManager.Instance.IsKeyDown(_keyboardMapping.Right) && !_hasHitTheWall)
                velocity.X = PlayerSpeed;
            else if (InputManager.Instance.IsKeyDown(_keyboardMapping.Left) && !_hasHitTheWall)
                velocity.X = -PlayerSpeed;
            else if (!_hasHitTheWall)
                velocity.X = 0f;

            if (InputManager.Instance.IsKeyDown(_keyboardMapping.Jump) && !_hasJumped)
            {
                position.Y -= JumpHeight;
                velocity.Y = -20f;
                _hasJumped = true;
                _effect.Play();
            }

            if (_hasJumped)
            {
                velocity.Y += Gravity;
                if (position.Y < _heightOfJump) _heightOfJump = position.Y;
            }

            if (position.Y + texture.Height >= clientBounds.Height)
            {
                _heightOfJump = clientBounds.Height;
                _hasJumped = false;
                _hasHitTheWall = false;
                position.Y = clientBounds.Height - texture.Height;
            }

            if (!_hasJumped)
                velocity.Y = 0f;
            
            if (position.X <= 0)
                if (_hasJumped)
                {
                    velocity.X = PlayerSpeed;
                    velocity.Y = -PlayerSpeed - PlayerSpeedChange;
                    _hasHitTheWall = true;
                }
                else 
                    position.X = 0;

            
            if (position.X >= (clientBounds.Width - texture.Width))
                if (_hasJumped)
                {
                    velocity.X = -PlayerSpeed;
                    velocity.Y = -PlayerSpeed - PlayerSpeedChange;
                    _hasHitTheWall = true;
                }
                else 
                    position.X = clientBounds.Width - texture.Width;


            //Update velocity and position
            Velocity = new Vector2(velocity.X, velocity.Y);
            Position = new Vector2(position.X + velocity.X, position.Y + velocity.Y);
 
            //Animate sprite
            base.Update(gameTime, clientBounds);
        }

        /// <summary>
        /// Runs when a players lands on a platform, making sure it is being displayed on the top of it.
        /// Also makes sure the variables got the correct value.
        /// </summary>
        /// <param name="floor"></param>
        public void LandedOnPlatForm(Platform platform)
        {
            //Må passe på at spilleren blir tegnet på toppen av platformen
            var newPosition = new Vector2(Position.X, (platform.Position.Y - texture.Height + 1));
            Position = newPosition;

            _hasJumped = false;
            _hasHitTheWall = false;
            HasHitPlatform = true;
            _heightOfJump = platform.Position.Y;
            Platform = platform;
        }

        public void WalkedOfPlatform()
        {
            HasHitPlatform = false;
            _hasJumped = true;
        }
        //On floor == platform
        public float GetY { get { return _heightOfJump; } }
    }
}
