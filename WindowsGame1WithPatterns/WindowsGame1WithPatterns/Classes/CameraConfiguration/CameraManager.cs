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

        /// <summary>
        /// Used to increase the speed of the camera
        /// </summary>
        private float _increaseSpeed;
        
        /// <summary>
        /// Used to start or stop the camera
        /// </summary>
        private bool _startCam;
        private Viewport _viewport;
        private Vector2 _center;
        private Matrix _transform;

        /// <summary>
        /// Defines how many pixels that is the top part of the window. Used to customize the speed.
        /// </summary>
        private const int TopPartOfWindow = 350;
        /// <summary>
        /// Defines how many pixels that is the middle part of the window. Used to customize the speed.
        /// </summary>
        private const int MiddlePartOfWindow = 200;
        /// <summary>
        /// Default speed depending on where the player is(the _increasedSpeed is multiplied to this)
        /// </summary>
        public readonly float DefaultStartSpeed = 3.0f;
        /// <summary>
        /// Default speed depending on where the player is(the _increasedSpeed is multiplied to this)
        /// </summary>
        private const float DefaultStartSpeedMiddle = 5.0f;
        /// <summary>
        /// Default speed depending on where the player is(the _increasedSpeed is multiplied to this)
        /// </summary>
        private const float DefaultStartSpeedTop = 6.0f;

        /// <summary>
        /// Describes how much camera velcocity shall be increased
        /// </summary>
        public readonly float VelocityDelta = 0.3f;

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
            _increaseSpeed += VelocityDelta;
        }

        /// <summary>
        /// Updates the camera position depending on where the player is located.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="gameTime"></param>
        public void Update(List<Vector2> position, GameTime gameTime)
        {
            //Check if the list is empty
            if (position.Count == 0) return;

            //Find the highest player
            var highestPlayer = position[0];
            foreach (var player in position)
                if (highestPlayer.Y > player.Y)
                    highestPlayer = player;

            //Here we check if player is on the top, middle or bottom part of the screen
            if (highestPlayer.Y < _center.Y - TopPartOfWindow)
                _velocity = (DefaultStartSpeedTop + _increaseSpeed) * -1;
            else if (highestPlayer.Y < _center.Y - MiddlePartOfWindow)
                _velocity = (DefaultStartSpeedMiddle + _increaseSpeed) * -1;
            else
                _velocity = (DefaultStartSpeed + _increaseSpeed) * -1;

            if (_startCam)
                _center.Y += _velocity;
            else
                _velocity = 0f;

            _transform = Matrix.CreateTranslation(new Vector3(-_center.X + (_viewport.Width / 2f),
                -_center.Y + (_viewport.Height / 2f), 0));
        }
    }
}