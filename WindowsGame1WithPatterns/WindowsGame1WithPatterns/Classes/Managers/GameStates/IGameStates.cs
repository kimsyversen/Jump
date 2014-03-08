using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1WithPatterns.Classes.Managers.GameStates
{
    public interface IGameStates
    {
        void GameOver();
        void InGame();
        void InMenu();
    }
}
