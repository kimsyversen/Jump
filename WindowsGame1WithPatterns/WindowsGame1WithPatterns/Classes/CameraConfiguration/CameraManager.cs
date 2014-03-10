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
        private Vector2 Center
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
        private bool test = false;
        public void Update(List<Vector2> position, int xOffset, int yOffset)
        {
            center.X = viewport.Width / 2;
            if (counter > 0)
            {
                center.Y = position[0].Y - (viewport.Height / 2) + 50;
                counter--;
            }
            Console.WriteLine(yOffset);
            if (center.Y < viewport.Height + viewport.Height/2 - yOffset)
            {
                velocity = 0f;
                return;
            }
           
            foreach (Vector2 p in position)
            {
                if (p.Y < center.Y - 200)
                {
                    test = true;
                    break;
                }
                test = false;
            }
            if(test)velocity = -1.2f;
            else velocity = -0.8f;
            
            center.Y += velocity;
            
            transform = Matrix.CreateTranslation(new Vector3(-center.X + (viewport.Width / 2),
                -center.Y + (viewport.Height / 2), 0));
        }
    }
}