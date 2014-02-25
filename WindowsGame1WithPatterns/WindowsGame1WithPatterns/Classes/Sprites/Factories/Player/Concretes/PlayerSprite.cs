using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Player.Concretes
{
    internal class PlayerSprite : Sprite, IPlayer
    {
        private Game _game;
        private readonly List<IFont> _observers;

        public PlayerSprite(Game game) : this(game.Content.Load<Texture2D>(@"Ball"), 
                new Vector2(game.Window.ClientBounds.Width / 2f, game.Window.ClientBounds.Height / 2f), new Point(30, 30), new Point(0, 0),
               new Point(0, 0), 0f, Vector2.Zero, 1f, SpriteEffects.None, new Vector2(0, 0), 0, 100)
        {
            _game = game;
            _observers = new List<IFont>();
        }

        public PlayerSprite(Texture2D texture, Vector2 spritePosition, Point frameSize, Point frameCurrent,
                            Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects,
                            Vector2 speed, int collisionOffset, int timeSinceLastFrame)
            : base(
                texture, spritePosition, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, speed,
                collisionOffset, timeSinceLastFrame)
        {
        }

        public PlayerSprite(Texture2D texture, Vector2 spritePosition, Point frameSize, Point frameCurrent,
                            Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects,
                            Vector2 speed, int collisionOffset, int millisecondsPerFrame, int timeSinceLastFrame)
            : base(
                texture, spritePosition, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, speed,
                collisionOffset, millisecondsPerFrame, timeSinceLastFrame)
        {
        }

        public new void Update(GameTime gameTime, Rectangle clientBounds)
        {
            const float playerSpeed = 4.0f;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Speed.X = -playerSpeed;
                SpritePosition.X += Speed.X;
                NotifyObservers();
            }
                
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Speed.X = playerSpeed;
                SpritePosition.X += Speed.X;
                NotifyObservers();
            }
                
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Speed.Y = -playerSpeed;
                SpritePosition.Y += Speed.Y;
                NotifyObservers();
            }
                
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Speed.Y = playerSpeed;
                SpritePosition.Y += Speed.Y;
                NotifyObservers();
            }

            //Make sure sprite is within screen
            var maxX = clientBounds.Width - FrameSize.X;
            var maxY = clientBounds.Height - FrameSize.Y;

            //Left
            if (SpritePosition.X < MinX)
                SpritePosition.X = 0;

            //Top
            if (SpritePosition.Y < MinY)
                SpritePosition.Y = 0;

            //Right
            if (SpritePosition.X > maxX)
                SpritePosition.X = clientBounds.Width - FrameSize.X;

            //Bottom
            if (SpritePosition.Y > maxY)
                SpritePosition.Y = clientBounds.Height - FrameSize.Y;


            //Animate sprite
            base.Update(gameTime, clientBounds);
        }

        public new void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Let the sprite class do it
            base.Draw(gameTime,spriteBatch);
        }
        #region ObserverPatternRelated

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
