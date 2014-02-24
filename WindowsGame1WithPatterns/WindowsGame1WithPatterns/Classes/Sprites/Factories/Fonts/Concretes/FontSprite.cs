using Microsoft.Xna.Framework;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts.Concretes
{
    class FontSprite : IFont
    {
        //Reference to the game. Needed because we need to get some properties like size of screen
        private Game _game;
        private int _score;

        //Reference to the player associated with this font
        private readonly IPlayer _subject;

        public FontSprite(Game game, IPlayer playerSubject)
        {
            _game = game;
            _subject = playerSubject;
            _subject.RegisterObserver(this);
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

        public void UpdateScore(int score)
        {
            _score++;
        }
    }
}
