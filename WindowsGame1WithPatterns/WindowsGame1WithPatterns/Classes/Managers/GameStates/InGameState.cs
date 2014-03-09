using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WindowsGame1WithPatterns.Classes.Managers.GameStates
{
    class InGameState : IGameStates
    {
        private Manager _manager;

        public InGameState(Manager manager)
        {
            _manager = manager;
        }

        public void GameOver()
        {
            throw new NotImplementedException();
        }


        public void InGame()
        {
            Debug.WriteLine("From " + GetType().Name + " Setting state to InMenuState");
            _manager.SetState(_manager.InMenuState);         
        }

        public void InMenu()
        {
            Debug.WriteLine("From " + GetType().Name + " Setting state to InMenuState");
            _manager.SetState(_manager.InMenuState);
        }
    }
}
