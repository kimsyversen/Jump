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
        //The velocity
        private float _velocity;
        //Start or stop camera.
        private bool _startCam;

        private float _increaseSpeed;
        private Viewport _viewport;

        //Constants that is used.
        //Coordinates used to customize the speed
        private const int TopPartOfWindow = 350;
        private const int MiddlePartOfWindow = 200;
        //Default speed depending on where the player is(the _increasedSpeed is multiplied to this)
        private const float DefaultStartSpeed = 3.0f;
        private const float DefaultStartSpeedMiddle = 5.0f;
        private const float DefaultStartSpeedTop = 6.0f;
        private int _faster;
        private Vector2 _center;
        private Matrix _transform;
        public float GetDefaultStartSpeed
        {
            get { return DefaultStartSpeed; }

        }

        //Velocity increasing
        public readonly float VelocityInc = 0.3f;

        public bool StartCam
        {
            set { _startCam = value; }
        }
        public float Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Matrix Transform
        {
            get { return _transform; }
        }
        
        public Vector2 Center
        {
            get { return _center; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="newViewport"></param>
        /// <param name="newVelocity"></param>
        public CameraManager(Viewport newViewport, float newVelocity)
        {
            _velocity = newVelocity;
            _viewport = newViewport;
            _center.Y = _viewport.Height / 2f;
            _center.X = _viewport.Width / 2f;
        }
    
        /// <summary>
        /// Increase speed when clearing a level. 
        /// </summary>
        public void IncreaseSpeed() 
        {
            _increaseSpeed += VelocityInc;
        }

        /// <summary>
        /// Updates the camera position depending on where the player is located.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="gameTime"></param>
        public void Update(List<Vector2> position, GameTime gameTime)
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
                case 1:
                    _velocity = (DefaultStartSpeedMiddle + _increaseSpeed) * -1;
                    break;
                case 2:
                    _velocity = (DefaultStartSpeedTop + _increaseSpeed) * -1;
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