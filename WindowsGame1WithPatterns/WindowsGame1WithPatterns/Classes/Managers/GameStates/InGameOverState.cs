using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WindowsGame1WithPatterns.Classes.Managers.GameStates
{
    class InGameOverState : IGameStates
    {
        private Manager _manager;

        public InGameOverState(Manager manager)
        {
            _manager = manager;
        }

        public void GameOverState()
        {
            Debug.WriteLine("From " + GetType().Name + " Setting state to InGamOverState");
            
            _manager.SetState(_manager.InGameOverState);
        }

        public void InGameOver()
        {
            throw new NotImplementedException();
        }

        public void InGame()
        {
            Debug.WriteLine("From " + GetType().Name + " Setting state to InGameState");
            _manager.SetState(_manager.InGameState);

        }

        public void InMenu()
        {
            Debug.WriteLine("From " + GetType().Name + " Setting state to InMenuState");

            //Used to change font in menu from resume to new game
            _manager.GameInProgress = 0;
            _manager.SetState(_manager.InMenuState);
        }

    }
}
