using WindowsGame1WithPatterns.Classes.KeyboardConfiguration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace WindowsGame1WithPatterns.Classes.Sprites.Concretes
{
    internal class Player : Sprite
    {
        /// <summary>
        /// Bool used to check if player jumped
        /// </summary>
        private bool _jumped;
        /// <summary>
        /// Bool used to check if player hits wall
        /// </summary>
        private bool _hitWall;
        /// <summary>
        /// Bool used to check if player is dead
        /// </summary>
        public bool Dead = false;
        /// <summary>
        /// Used outside to decide if player hits platform
        /// </summary>
        public bool HitPlatform { get; set; }
        /// <summary>
        /// Contains score for the player
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// Used as reference for a platform player lands on
        /// </summary>
        public Platform Platform { get; set; }
        /// <summary>
        /// Used to set/get height position from the position of a platform
        /// </summary>
        public float JumpHeight { get; set; }
        /// <summary>
        /// Gravity in the game. Higher positive value makes it harder to jump
        /// </summary>
        private const float Gravity = 1.15f;
        /// <summary>
        /// How high a player may jump
        /// </summary>
        private const float PlayerJumpHeight = 10f;     
        /// <summary>
        /// Velocity the player moves with
        /// </summary>
        private const float PlayerVelocity = 5.0f;
        /// <summary>
        /// When player hits wall, player bounces back with velocity
        /// </summary>
        private const float BounceBackVelocity = 5.0f;
        /// <summary>
        /// Reference for sound effect
        /// </summary>
        private readonly SoundEffect _effect;
        /// <summary>
        /// Used to assign keys to players
        /// </summary>
        private readonly KeyboardMapping _keyboardMapping;

        public Player(Game game, String textureName, KeyboardMapping keyboardMapping, Vector2 position)
            : this(game, game.Content.Load<Texture2D>(textureName),
                new Vector2(game.Window.ClientBounds.Width / 2f, game.Window.ClientBounds.Height - 48), new Point(48, 48), new Point(0, 0),
                new Point(0, 0), 0f, Vector2.Zero, 1f, SpriteEffects.None, new Vector2(0, 0), 0, 100)
        {

             Dead = false;
            _jumped = true;
            _hitWall = false;
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
            //Check if player is dead
            if (Dead)
            {
                velocity.Y = 0f;
                velocity.X = 0f;
                return;
            }

            //Check player input, decide velocity
            if (InputManager.Instance.IsKeyDown(_keyboardMapping.Right) && !_hitWall)
                velocity.X = PlayerVelocity;
            else if (InputManager.Instance.IsKeyDown(_keyboardMapping.Left) && !_hitWall)
                velocity.X = -PlayerVelocity;
            else if (!_hitWall)
                velocity.X = 0f;

            //Check if player can jump
            if (InputManager.Instance.IsKeyDown(_keyboardMapping.Jump) && !_jumped)
            {
                position.Y -= PlayerJumpHeight;
                velocity.Y = -20f;
                _jumped = true;
                _effect.Play();
            }

            //Check if player have jumped
            if (_jumped)
            {
                velocity.Y += Gravity;
                if (position.Y < JumpHeight)
                    JumpHeight = position.Y;
            }

            //Check if player is within screen
            if (position.Y + texture.Height >= clientBounds.Height)
            {
                JumpHeight = clientBounds.Height;
                _jumped = false;
                _hitWall = false;
                position.Y = clientBounds.Height - texture.Height;
            }

            if (!_jumped)
                velocity.Y = 0f;
            //Check if player is within screen
            if (position.X <= 0)
                if (_jumped)
                {
                    velocity.X = PlayerVelocity;
                    velocity.Y = -BounceBackVelocity;
                    _hitWall = true;
                }
                else 
                    position.X = 0;

            //Check if player is within screen
            if (position.X >= (clientBounds.Width - texture.Width))
                if (_jumped)
                {
                    velocity.X = -PlayerVelocity;
                    velocity.Y = -BounceBackVelocity;
                    _hitWall = true;
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
        /// <param name="platform"></param>
        public void LandedOnPlatForm(Platform platform)
        {
            //Make sure player is drawn on top of a platform
            var newPosition = new Vector2(Position.X, (platform.Position.Y - texture.Height + 1));
            Position = newPosition;
            _jumped = false;
            _hitWall = false;
            HitPlatform = true;
            JumpHeight = platform.Position.Y;
            Platform = platform;
        }

        public void WalkedOfPlatform()
        {
            HitPlatform = false;
            _jumped = true;
        }
    }
}
