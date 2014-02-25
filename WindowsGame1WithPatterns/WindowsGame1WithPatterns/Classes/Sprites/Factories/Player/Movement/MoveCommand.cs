using Microsoft.Xna.Framework;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Player.Movement
{
    class MoveCommand : ICommand
    {
        private readonly IPlayer _player;
        private readonly Vector2 _speed;
        private readonly Vector2 _position;

        public MoveCommand(IPlayer player, Vector2 speed, Vector2 position)
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
