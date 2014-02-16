using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.Sprites
{
    abstract class Sprite
    {
        //TODO: Følge konvensjonen her
        protected const int DefaultMillisecondsPerFrame = 16; //60 times per second
        protected int _collisionOffset; //pixels outside of texture to not collide with
        protected Point _frameCurrent; //Index (x,y) for current frame in spritesheet/sprite
        protected Point _frameSize; //Size for each individual frame in spritesheet/sprite
        protected int _millisecondsPerFrame; //Milliseconds between framechanges
        protected Vector2 _position; // Position of sprite
        protected Point _sheetSize; // Number of columns/rows in sprite sheet
        protected Vector2 _speed; // Speed of sprite
        protected int _timeSinceLastFrame = 0; //Milliseconds since last frame was drawn
        protected Texture2D _texture;
        protected SpriteEffects _spriteEffects;
        protected float _scale;
        protected float _rotate;
        protected Vector2 _origin; //Origin for rotation

        private Random _random;
        protected const int minX = 0;
        protected const int minY = 0;

        public abstract Vector2 Direction { get; }

        public Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle
                    (
                    (int)_position.X + _collisionOffset,
                    (int)_position.Y + _collisionOffset,
                    _frameSize.X - (_collisionOffset * 2),
                    _frameSize.Y - (_collisionOffset * 2)
                    );
            }
        }
        protected Sprite(Texture2D texture, Vector2 position, Point frameSize, Point frameCurrent, Point sheetSize,
   float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects, Vector2 speed, int collisionOffset, int timeSinceLastFrame)
            : this(texture, position, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, speed, collisionOffset, timeSinceLastFrame, DefaultMillisecondsPerFrame) { }


        protected Sprite(Texture2D texture, Vector2 position, Point frameSize, Point frameCurrent, Point sheetSize,
            float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects, Vector2 speed, int collisionOffset, int millisecondsPerFrame, int timeSinceLastFrame)
        {
            _collisionOffset = collisionOffset;
            _frameCurrent = frameCurrent;
            _frameSize = frameSize;
            _millisecondsPerFrame = millisecondsPerFrame;
            _position = position;
            _sheetSize = sheetSize;
            _speed = speed;
            _texture = texture;
            _timeSinceLastFrame = timeSinceLastFrame;
            _spriteEffects = spriteEffects;
            _scale = scale;
            _rotate = rotate;
            _origin = origin;
        }
         protected Sprite() {}
        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            //Animation

            //Count time since last frame
            _timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            //If its not time to update frame, return
            if (_timeSinceLastFrame <= _millisecondsPerFrame) return;

            _timeSinceLastFrame -= _millisecondsPerFrame;

            //Logic for choosing frames from spritesheets
            ++_frameCurrent.X;
            if (_frameCurrent.X < _sheetSize.X) return;

            _frameCurrent.X = 0;
            ++_frameCurrent.Y;
            if (_frameCurrent.Y >= _sheetSize.Y)
                _frameCurrent.Y = 0;

            //Make sure sprite is within screen
            var maxX = clientBounds.Width - _frameSize.X;
            var maxY = clientBounds.Height - _frameSize.Y;

            //Left
            if (_position.X < minX)
                _position.X = 0;

            //Top
            if (_position.Y < minY)
                _position.Y = 0;

            //Right
            if (_position.X > maxX)
                _position.X = clientBounds.Width - _frameSize.X;

            //Bottom
            if (_position.Y > maxY)
                _position.Y = clientBounds.Height - _frameSize.Y;

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture,
                _position,
                new Rectangle(_frameCurrent.X * _frameSize.X, _frameCurrent.Y * _frameSize.Y, _frameSize.X, _frameSize.Y),
                Color.White, _rotate, _origin,
                _scale, _spriteEffects, 0);
        }

        public float RandomFloat(double minimum = 6, double maximum = 25)
        {
            _random = new Random();
            return (float)(_random.NextDouble() * (maximum - minimum) + minimum);
        }  
    }
}
