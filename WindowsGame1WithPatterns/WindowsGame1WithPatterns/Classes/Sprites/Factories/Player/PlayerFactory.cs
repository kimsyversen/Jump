using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player.Concretes;
using WindowsGame1WithPatterns.Classes.KeyboardConfiguration;
namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Player
{
    class PlayerFactory : PlayerCreator
    {
        private Game _game;

        public PlayerFactory(Game game)
        {
            _game = game;
        }

        public override IPlayer CreatePlayerOne()
        {
            return new Concretes.Player(_game, new KeyboardMapping(Keys.A, Keys.D, Keys.Space));
        }

        public override IPlayer CreatePlayerTwo()
        {
            return new Concretes.Player(_game, new KeyboardMapping(Keys.Left, Keys.Right, Keys.Up));
        }
    }
}
