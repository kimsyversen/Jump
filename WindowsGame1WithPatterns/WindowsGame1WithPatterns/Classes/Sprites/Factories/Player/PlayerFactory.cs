using Microsoft.Xna.Framework;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player.Concretes;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Player
{
    class PlayerFactory : PlayerCreator
    {
        public override IPlayer CreatePlayerOne(Game game)
        {
            return new PlayerSprite(game);
        }

        public override IPlayer CreatePlayerTwo(Game game)
        {
            return new PlayerSprite(game);
        }
    }
}
