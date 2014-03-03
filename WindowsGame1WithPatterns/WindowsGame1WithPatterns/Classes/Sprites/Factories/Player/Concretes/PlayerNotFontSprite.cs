﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player.Movement;
using System;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Player.Concretes
{
    internal class PlayerNotFontSprite : NotFontSprite, IPlayer
    {
        private Game _game;
        private readonly List<IFont> _observers;
        bool hasJumped;
        bool hasHitTheWall;

        public PlayerNotFontSprite(Game game) : this(game.Content.Load<Texture2D>(@"Ball"), 
                new Vector2(game.Window.ClientBounds.Width / 2f, game.Window.ClientBounds.Height / 2f), new Point(30, 30), new Point(0, 0),
               new Point(0, 0), 0f, Vector2.Zero, 1f, SpriteEffects.None, new Vector2(0, 0), 0, 100)
        {
            _game = game;
            _observers = new List<IFont>();
            hasJumped = true;
            hasHitTheWall = false;
        }




        public PlayerNotFontSprite(Texture2D texture, Vector2 spritePosition, Point frameSize, Point frameCurrent,
                            Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects,
                            Vector2 speed, int collisionOffset, int timeSinceLastFrame)
            : base(
                texture, spritePosition, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, speed,
                collisionOffset, timeSinceLastFrame)
        {
        }

        public PlayerNotFontSprite(Texture2D texture, Vector2 spritePosition, Point frameSize, Point frameCurrent,
                            Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects,
                            Vector2 speed, int collisionOffset, int millisecondsPerFrame, int timeSinceLastFrame)
            : base(
                texture, spritePosition, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, speed,
                collisionOffset, millisecondsPerFrame, timeSinceLastFrame)
        {
        }
        const float playerSpeed = 3.0f;
        public new void Update(GameTime gameTime, Rectangle clientBounds)
        {


            if (Keyboard.GetState().IsKeyDown(Keys.D) && hasHitTheWall == false) Speed.X = playerSpeed;
            else if (Keyboard.GetState().IsKeyDown(Keys.A) && hasHitTheWall == false) Speed.X = -playerSpeed; else if (hasHitTheWall == false) Speed.X = 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                SpritePosition.Y -= 10f;
                Speed.Y = -20f;
                hasJumped = true;
            }
            if (hasJumped == true)
            {
                float i = 1;
                Speed.Y += 0.15f + i;
            }
            if (SpritePosition.Y + Texture.Height > clientBounds.Height)
            {
                hasJumped = false;
                hasHitTheWall = false;
            }

            if (hasJumped == false)
                Speed.Y = 0f;

            if (SpritePosition.X <= 0)
            {
                if(hasJumped == true){
                    Speed.X = playerSpeed;
                    Speed.Y = -playerSpeed - 5;
                    hasHitTheWall = true;
                }else SpritePosition.X =0;
                
            }
            if (SpritePosition.X >= (clientBounds.Width - Texture.Width))
            {
                if (hasJumped == true)
                {
                    Speed.X = -playerSpeed;
                    Speed.Y = -playerSpeed - 5;
                    hasHitTheWall = true;
                }
                else SpritePosition.X = clientBounds.Width - Texture.Width;   
            }

            //Bruker MoveCommand for flyttingen, og gir beskjed til observer
            var cmd = new MoveCommand(this, new Vector2(Speed.X, Speed.Y), new Vector2(SpritePosition.X + Speed.X, SpritePosition.Y + Speed.Y));
            cmd.Execute();
            NotifyObservers();

            //Animate sprite
            base.Update(gameTime, clientBounds);
        }

        public new void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Let the sprite class do it
            base.Draw(gameTime,spriteBatch);
        }
        

        public Vector2 PlayerSpeed
        {
            get { return Speed; }
            set { Speed = value; }
        }

        public Vector2 PlayerPosition
        {
            get { return SpritePosition; }
            set { SpritePosition = value; }
        }

        #region ObserverPatternRelated

        public void UpdateBlabla()
        {
            throw new System.NotImplementedException();
        }

        public void RegisterObserver(IFont observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IFont observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
             foreach (var observer in _observers)
                observer.UpdateCoordinates(this.SpritePosition);
        }

        #endregion
    }
}