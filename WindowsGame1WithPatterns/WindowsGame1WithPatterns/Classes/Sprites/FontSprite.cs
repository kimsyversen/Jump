using Microsoft.Xna.Framework;
using WindowsGame1WithPatterns.Classes.Sprites.Factory;

namespace WindowsGame1WithPatterns.Classes.Sprites
{
    class FontSprite : ISprite
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
    }
}
