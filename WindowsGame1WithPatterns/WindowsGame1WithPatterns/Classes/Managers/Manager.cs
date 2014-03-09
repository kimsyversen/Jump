using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WindowsGame1WithPatterns.Classes.Managers;
using WindowsGame1WithPatterns.Classes.Managers.GameStates;

namespace WindowsGame1WithPatterns.Classes.Managers
{
    class Manager : DrawableGameComponent, IGameStates
    {
        public int GameInProgress { get; set; }
        private readonly IGameStates _inGameState;
        private readonly IGameStates _inMenuState;
        private readonly IGameStates _inGameOverState;
        private IGameStates _currentState;

        private InGameManager _inGameManager;
        private InMenuManager _inMenuManager;
        private InGameOverManager _inGameOverManager;
        #region properties
        public IGameStates InGameState
        {
            get { return _inGameState; }
        }

        public IGameStates InMenuState
        {
            get { return _inMenuState; } 
        }
        public IGameStates InGameOverState
        {
            get { return _inGameOverState; }
        }
        public IGameStates CurrentState
        {
            get { return _currentState; }
        }
        public InGameManager InGameManager
        {
            get { return _inGameManager; }
            set { _inGameManager = value; }
        }
        public InMenuManager InMenuManager
        {
            get { return _inMenuManager; }
            set { _inMenuManager = value; }
        }
        #endregion
        public Manager(Game game) : base(game)
        {
            _inGameManager = new InGameManager(Game, this);
            _inMenuManager =  new InMenuManager(Game, this);
            _inGameOverManager = new InGameOverManager(Game, this);

            _inGameState = new InGameState(this);
            _inMenuState = new InMenuState(this);
            _inGameOverState = new InGameOverState(this);

            //Add components to game loop
            Game.Components.Add(_inGameManager);
            Game.Components.Add(_inMenuManager);
            Game.Components.Add(_inGameOverManager);

            //Start game in menustate
            SetState(_inMenuState);
            InMenu();
        }

        //Sets the new state, displays/hides the right manager.
        public void SetState(IGameStates state)
        {
            _currentState = state;

            if (_currentState == InGameState)
            {
                _inGameOverManager.Enable(false);
                _inMenuManager.Enable(false);
                _inGameManager.Enable(true);
            }
            else if (_currentState == InMenuState)
            {
                _inGameOverManager.Enable(false);
                _inGameManager.Enable(false);
                _inMenuManager.Enable(true);
                
            }
            else if (_currentState == _inGameOverState)
            {
                _inMenuManager.Enable(false);
                _inGameManager.Enable(false);
                _inGameOverManager.Enable(true);
            }
        }

        public void InGameOver()
        {
            _currentState.InGameOver();
        }

        public void InGame()
        {
           _currentState.InGame();
        }

        public void InMenu()
        {
            _currentState.InMenu();
        }


        public void Enable(bool value)
        {
            Enabled = value;
            Visible = value;
        }
    }
}
