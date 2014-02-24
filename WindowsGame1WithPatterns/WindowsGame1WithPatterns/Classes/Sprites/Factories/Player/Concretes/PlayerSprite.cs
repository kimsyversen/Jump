using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Player.Concretes
{
    class PlayerSprite : Sprite, IPlayer
    {
        private Game _game;
        private string _name;
        private readonly List<IFont> _observers;


        public PlayerSprite(Game game)
        {
            _game = game;
            _name = "Player Sprite";
            _observers = new List<IFont>();
        }

        public PlayerSprite(Texture2D texture, Vector2 spritePosition, Point frameSize, Point frameCurrent, Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects, Vector2 speed, int collisionOffset, int timeSinceLastFrame) : base(texture, spritePosition, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, speed, collisionOffset, timeSinceLastFrame)
        {
        }

        public PlayerSprite(Texture2D texture, Vector2 spritePosition, Point frameSize, Point frameCurrent, Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects, Vector2 speed, int collisionOffset, int millisecondsPerFrame, int timeSinceLastFrame) : base(texture, spritePosition, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, speed, collisionOffset, millisecondsPerFrame, timeSinceLastFrame)
        {
        }

        public void CreateSprite()
        {
            throw new System.NotImplementedException();
        }

        public Vector2 Position
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public void Test()
        {
            throw new System.NotImplementedException();
        }
        public override Vector2 Direction
        {
            get { throw new System.NotImplementedException(); }
        }



        //Observer pattern related
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
            // foreach (var observer in _observers)
            // observer.Update(_number1, _number2);
        }
    }
}
