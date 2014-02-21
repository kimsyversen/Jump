using Microsoft.Xna.Framework;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Player
{
    abstract class PlayerCreator
    {
        public abstract IPlayer CreatePlayerOne();
        public abstract IPlayer CreatePlayerTwo();
    }
}
