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
        private bool _startCam ;
        private const float VelocityInc = 0.2f;
        private float _increaseSpeed;
        private Viewport _viewport;
        private int _counter;

        //TODO: Hva brukes denne til?
        private int _windowHeight;

        private int _faster = 0;
        private Vector2 _center;
        public bool StartCam
        {
            set { _startCam = value; }
        }
        public float Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }
        private Matrix _transform;
        public Matrix Transform
        {
            get { return _transform; }
        }
        
        public Vector2 Center
        {
            get { return _center; }
        }


        public CameraManager(Viewport newViewport, float newVelocity, int newWindowHeight)
        {
            _velocity = newVelocity;
            _viewport = newViewport;
            _counter = 1;
            _windowHeight = newWindowHeight;
        }
    

        public void IncreaseSpeed() 
        {
            _increaseSpeed += VelocityInc;
        }

        public void Update(List<Vector2> position, int xOffset, int yOffset, GameTime gameTime)
        {
            _center.X = _viewport.Width / 2f;
            if (_counter > 0)
            {
                _center.Y = position[0].Y - (_viewport.Height / 2f) + 50;
                _counter--;
            }
            if (_center.Y < _viewport.Height + _viewport.Height / 2 - yOffset - 40)
            {
                _velocity = 0f;
                return;
            }

            foreach (Vector2 p in position)
            {
                if (p.Y < _center.Y - 350)
                {
                    _faster = 2;
                    break;
                }
                if (p.Y < _center.Y - 200)
                {
                    _faster = 1;
                    break;
                }
                _faster = 0;
            }
            //TODO: 3.0, 2.0 og 1.2 - hva er disse?
            switch (_faster)
            {
                case 2:
                    _velocity = ((((float)gameTime.ElapsedGameTime.TotalMilliseconds / 15) * 3.0f)+_increaseSpeed)* - 1;
                    break;
                case 1:
                    _velocity = ((((float)gameTime.ElapsedGameTime.TotalMilliseconds/15) * 2.0f)+_increaseSpeed)* -1;
                    break;
                default:
                    _velocity = ((((float)gameTime.ElapsedGameTime.TotalMilliseconds / 15) * 1.2f) + _increaseSpeed) * -1;
                    break;
            }

            if (_startCam)
                _center.Y += _velocity;

            _transform = Matrix.CreateTranslation(new Vector3(-_center.X + (_viewport.Width / 2f),
                -_center.Y + (_viewport.Height / 2f), 0));
        }
    }
}