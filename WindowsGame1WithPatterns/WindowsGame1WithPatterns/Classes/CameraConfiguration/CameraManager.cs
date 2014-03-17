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
        private const float VelocityInc = 0.3f;
        private float _increaseSpeed;
        private Viewport _viewport;

        private const int TopPartOfWindow = 350;
        private const int MiddlePartOfWindow = 200;
        private const float DefaultStartSpeed = 3.0f;
        private const float DefaultStartSpeedMiddle = 5.0f;
        private const float DefaultStartSpeedTop = 6.0f;

        private int _faster;
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


        public CameraManager(Viewport newViewport, float newVelocity)
        {
            _velocity = newVelocity;
            _viewport = newViewport;
            _center.Y = _viewport.Height / 2f;
            _center.X = _viewport.Width / 2f;
        }
    

        public void IncreaseSpeed() 
        {
            _increaseSpeed += VelocityInc;
        }

        public void Update(List<Vector2> position, int xOffset, int yOffset, GameTime gameTime)
        {
            foreach (var p in position)
            {
                //Here we check if one player is on the top, middle or bottom part of the screen
                if (p.Y < _center.Y - TopPartOfWindow)
                {
                    _faster = 2;
                    break;
                }
                if (p.Y < _center.Y - MiddlePartOfWindow)
                {
                    _faster = 1;
                    break;
                }
                _faster = 0;
            }
            //Set the velocity 
            switch (_faster)
            {
                case 2:
                    _velocity = (DefaultStartSpeedTop + _increaseSpeed) * -1;
                    break;
                case 1:
                    _velocity = (DefaultStartSpeedMiddle + _increaseSpeed) * -1;
                    break;
                default:
                    _velocity = (DefaultStartSpeed + _increaseSpeed) * -1;
                    break;
            }

           

            if (_startCam)
                _center.Y += _velocity;
            else
                _velocity = 0f;

            _transform = Matrix.CreateTranslation(new Vector3(-_center.X + (_viewport.Width / 2f),
                -_center.Y + (_viewport.Height / 2f), 0));
        }
    }
}