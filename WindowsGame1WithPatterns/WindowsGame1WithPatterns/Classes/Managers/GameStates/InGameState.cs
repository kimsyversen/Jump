using System;
using System.Collections.Generic;
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

        private void SwitchState(bool value)
        {
            _manager.InMenuManager.Visible = !value;
            _manager.InMenuManager.Visible = !value;

            _manager.InGameManager.Visible = value;
            _manager.InGameManager.Visible = value;
        }

        public void InGame()
        {
            _manager.SetState(_manager.InMenuState);
            SwitchState(true);
        }

        public void InMenu()
        {
            _manager.SetState(_manager.InGameState);
            SwitchState(false);
        }
    }
}
