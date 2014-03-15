using Microsoft.Xna.Framework;

namespace WindowsGame1WithPatterns.Classes.Sprites.Concretes.Movement
{
    /// <summary>
    /// By using command pattern, its possible to create a replay of the game very easy. This functionality is not implemented.
    /// </summary>
    class MoveCommand : ICommand
    {
        private readonly Player _player;
        private readonly Vector2 _speed;
        private readonly Vector2 _position;

        public MoveCommand(Player player, Vector2 speed, Vector2 position)
        {
            _player = player;
            _speed = speed;
            _position = position;
        }

        public void Execute()
        {
            _player.PlayerSpeed = _speed;
            _player.PlayerPosition = _position;
        }  
    }
}
