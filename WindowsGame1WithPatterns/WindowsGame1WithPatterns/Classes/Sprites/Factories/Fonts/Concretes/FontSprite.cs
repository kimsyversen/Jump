using Microsoft.Xna.Framework;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts.Concretes
{
    class FontSprite : IFont
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
    }
}
