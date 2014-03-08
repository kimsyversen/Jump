using System;
using System.Collections.Generic;
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

        public void GameOver()
        {
            throw new NotImplementedException();
        }

        public void InGame()
        {
            _manager.SetState(_manager.InGameState);

            //Used to change font in menu from new game to resume
            _manager.GameInProgress = 1;
            SwitchState(true);
        }

        public void InMenu()
        {
            _manager.SetState(_manager.InMenuState);
        }

        private void SwitchState(bool value)
        {
            _manager.InMenuManager.Visible = !value;
            _manager.InMenuManager.Visible = !value;

            _manager.InGameManager.Visible = value;
            _manager.InGameManager.Visible = value;
        }
    }
}
