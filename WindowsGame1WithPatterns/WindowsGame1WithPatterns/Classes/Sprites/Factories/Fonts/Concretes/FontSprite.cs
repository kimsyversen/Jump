using Microsoft.Xna.Framework;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts.Concretes
{
    class FontSprite<T> : IFont, IColleague<T>
    {
        private Game _game;

        public FontSprite(Game game)
        {
            _game = game;
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
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public void SendMessage(IMediator<T> mediator, T message)
        {
            throw new System.NotImplementedException();
        }

        public void ReceiveMessage(T message)
        {
            throw new System.NotImplementedException();
        }
    }
}
