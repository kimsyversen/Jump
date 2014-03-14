using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.CameraConfiguration
{
    public class CameraManager
    {
        private float _velocity;
        private bool _startCam = false;
        private const float VelocityInc = 0.2f;
        private float _increaseSpeed = 0.0f;
        private Viewport _viewport;
        private int _counter;
        private int _windowHeight;
        private int _faster = 0;
        public bool StartCam
        {
            set { _startCam = value; }
        }
        public float Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }
        private Matrix transform;
        public Matrix Transform
        {
            get { return transform; }
        }
        private Vector2 center;
        public Vector2 Center
        {
            get { return center; }
        }


        public CameraManager(Viewport newViewport, float newVelocity, int newWindowHeight)
        {
            _velocity = newVelocity;
            _viewport = newViewport;
            _counter = 1;
            _windowHeight = newWindowHeight;
        }
    

        public void IncreaseSpeed() {
            _increaseSpeed += VelocityInc;
        }

        public void Update(List<Vector2> position, int xOffset, int yOffset, GameTime gameTime)
        {
            center.X = _viewport.Width / 2f;
            if (_counter > 0)
            {
                center.Y = position[0].Y - (_viewport.Height / 2f) + 50;
                _counter--;
            }
            if (center.Y < _viewport.Height + _viewport.Height / 2 - yOffset - 40)
            {
                _velocity = 0f;
                return;
            }

            foreach (Vector2 p in position)
            {
                if (p.Y < center.Y - 350)
                {
                    _faster = 2;
                    break;
                }
                if (p.Y < center.Y - 200)
                {
                    _faster = 1;
                    break;
                }
                _faster = 0;
            }
            if (_faster == 2) 
                _velocity = ((((float)gameTime.ElapsedGameTime.TotalMilliseconds / 15) * 3.0f)+_increaseSpeed)* - 1;
            else if (_faster == 1) 
                _velocity = ((((float)gameTime.ElapsedGameTime.TotalMilliseconds/15) * 2.0f)+_increaseSpeed)* -1;
            else
                _velocity = ((((float)gameTime.ElapsedGameTime.TotalMilliseconds / 15) * 1.2f) + _increaseSpeed) * -1;

            if (_startCam)
                center.Y += _velocity;

            transform = Matrix.CreateTranslation(new Vector3(-center.X + (_viewport.Width / 2f),
                -center.Y + (_viewport.Height / 2f), 0));
        }
    }
}