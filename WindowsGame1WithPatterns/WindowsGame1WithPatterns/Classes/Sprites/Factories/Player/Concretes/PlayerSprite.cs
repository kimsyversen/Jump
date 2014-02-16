using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Concretes
{
    class PlayerSprite : Sprite, IPlayer
    {
        private Game _game;
        private string _name;
        public PlayerSprite(Game game)
        {
            _game = game;
            _name = "Player Sprite";
        }
        public PlayerSprite(Texture2D texture, Vector2 position, Point frameSize, Point frameCurrent, Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects, Vector2 speed, int collisionOffset, int timeSinceLastFrame) : base(texture, position, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, speed, collisionOffset, timeSinceLastFrame)
        {
        }

        public PlayerSprite(Texture2D texture, Vector2 position, Point frameSize, Point frameCurrent, Point sheetSize, float rotate, Vector2 origin, float scale, SpriteEffects spriteEffects, Vector2 speed, int collisionOffset, int millisecondsPerFrame, int timeSinceLastFrame) : base(texture, position, frameSize, frameCurrent, sheetSize, rotate, origin, scale, spriteEffects, speed, collisionOffset, millisecondsPerFrame, timeSinceLastFrame)
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
    }
}
