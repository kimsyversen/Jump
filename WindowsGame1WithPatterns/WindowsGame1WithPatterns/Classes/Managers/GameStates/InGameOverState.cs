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
            Debug.WriteLine("From " + GetType().Name + " Setting state to InGameOverState");
            _manager.SetState(_manager.InGameOverState);
        }

        public void InGameOver()
        {

        }

        public void InGame()
        {
            Debug.WriteLine("From " + GetType().Name + " Setting state to InGameState");
            //Used to change font in menu from resume to new game
            _manager.GameInProgress = 0;
            _manager.SetState(_manager.InGameState);

        }

        public void InMenu()
        {
            Debug.WriteLine("From " + GetType().Name + " Setting state to InMenuState");

            _manager.SetState(_manager.InMenuState);
        }

    }
}
