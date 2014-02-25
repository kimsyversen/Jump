﻿using Microsoft.Xna.Framework;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player.Concretes;

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
            return new PlayerSprite(_game);
        }

        public override IPlayer CreatePlayerTwo()
        {
            return new PlayerSprite(_game);
        }
    }
}