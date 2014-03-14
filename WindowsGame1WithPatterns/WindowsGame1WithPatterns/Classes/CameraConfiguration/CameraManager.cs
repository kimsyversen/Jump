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
        private float velocity;
        private bool startCam = false;
        private const float VelocityInc = 0.2f;
        private float increaseSpeed = 0.0f;

        public bool StartCam
        {
            set { startCam = value; }
        }
        public float Velocity
        {
            get { return velocity; }
            set { velocity = value; }
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
        private Viewport viewport;
        private int counter;
        private int windowHeight;

        public CameraManager(Viewport newViewport, float newVelocity, int newWindowHeight)
        {
            velocity = newVelocity;
            viewport = newViewport;
            counter = 1;
            windowHeight = newWindowHeight;
        }

        //private Vector2 vector;
       // private bool faster = false;
        private int faster = 0;

        public void IncreaseSpeed() {
            increaseSpeed += VelocityInc;
        }

        public void Update(List<Vector2> position, int xOffset, int yOffset, GameTime gameTime)
        {
            center.X = viewport.Width / 2;
            if (counter > 0)
            {
                center.Y = position[0].Y - (viewport.Height / 2) + 50;
                counter--;
            }
            if (center.Y < viewport.Height + viewport.Height / 2 - yOffset - 40)
            {
                velocity = 0f;
                return;
            }

            foreach (Vector2 p in position)
            {
                if (p.Y < center.Y - 350)
                {
                    faster = 2;
                    break;
                }
                if (p.Y < center.Y - 200)
                {
                    faster = 1;
                    break;
                }
                faster = 0;
            }
            if (faster == 2) velocity = ((((float)gameTime.ElapsedGameTime.TotalMilliseconds / 15) * 3.0f)+increaseSpeed)* - 1;
            else if (faster == 1) velocity = ((((float)gameTime.ElapsedGameTime.TotalMilliseconds/15) * 2.0f)+increaseSpeed)* -1;
            else velocity = ((((float)gameTime.ElapsedGameTime.TotalMilliseconds / 15) * 1.2f) + increaseSpeed) * -1;

            if (startCam)
                center.Y += velocity;

            transform = Matrix.CreateTranslation(new Vector3(-center.X + (viewport.Width / 2),
                -center.Y + (viewport.Height / 2), 0));
        }
    }
}