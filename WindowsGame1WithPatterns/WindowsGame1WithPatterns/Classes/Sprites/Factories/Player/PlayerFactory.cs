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
            return new Concretes.Player(_game, true, "Figure/lilastoy");
        }

        public override IPlayer CreatePlayerTwo()
        {
            return new Concretes.Player(_game, false, "Figure/greenstoy");
        }
    }
}
