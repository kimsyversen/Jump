using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WindowsGame1WithPatterns.Classes.Managers.GameStates
{
    class InMenuState : IGameStates
    {
        private Manager _manager;

        public InMenuState(Manager manager)
        {
            _manager = manager;
        }

        public void InGameOver()
        {
            //TODO: This, is normally a bad design choise. I've made a compromise here.
            Debug.WriteLine("This is not possible.");
        }

        public void InGame()
        {
            Debug.WriteLine("From " + GetType().Name + " Setting state to InGameState");

            //Used to change font in menu from new game to resume
            _manager.GameInProgress = 1;
            _manager.SetState(_manager.InGameState);

        }

        public void InMenu()
        {
            Debug.WriteLine("From " + GetType().Name + " Setting state to InMenuState");
            _manager.SetState(_manager.InMenuState);
        }

    }
}
