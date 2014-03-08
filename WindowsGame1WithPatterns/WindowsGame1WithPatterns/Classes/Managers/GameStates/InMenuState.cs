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

            _manager.InMenuManager.Visible = false;
            _manager.InMenuManager.Visible = false;

            _manager.InGameManager.Visible = true;
            _manager.InGameManager.Visible = true;
        }

        public void InMenu()
        {
            _manager.SetState(_manager.InMenuState);
        }
    }
}
