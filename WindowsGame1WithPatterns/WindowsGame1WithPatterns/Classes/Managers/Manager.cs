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
        private IGameStates _currentState;

        private InGameManager _inGameManager;
        private InMenuManager _inMenuManager;

        #region properties
        public IGameStates InGameState
        {
            get { return _inGameState; }
        }

        public IGameStates InMenuState
        {
            get { return _inMenuState; } 
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

            _inGameState = new InGameState(this);
            _inMenuState = new InMenuState(this);

            //Add components to game loop
            Game.Components.Add(_inGameManager);
            Game.Components.Add(_inMenuManager);

            //Start game in menustate
            SetState(_inMenuState);
            InMenu();
        }

        //Sets the new state, displays/hides the right manager
        public void SetState(IGameStates state)
        {
            _currentState = state;

            if (_currentState == InGameState)
            {
                _inMenuManager.Enable(false);
                _inGameManager.Enable(true);
            }
            else if (_currentState == InMenuState)
            {
                _inMenuManager.Enable(true);
                _inGameManager.Enable(false);
            }
        }

        public void GameOver()
        {
            throw new NotImplementedException();
        }

        public void InGame()
        {
           _currentState.InGame();
        }

        public void InMenu()
        {
            _currentState.InMenu();
        }


    }
}
