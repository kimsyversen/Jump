using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WindowsGame1WithPatterns.Classes.Managers;

namespace WindowsGame1WithPatterns.Classes.Managers
{
    class Manager : DrawableGameComponent
    {
        private InGameManager _inGameManager;
        private MenuManager _menuManager;
        private GameState _currentGameState;
        private GameState _oldGameState;

        public GameState CurrentGameState
        {
            get { return _currentGameState; }
            set
            { 
                _currentGameState = value; 
                SwitchState();
            }
        }

        public GameState OldGameState
        {
            get { return _oldGameState; }
            set { _oldGameState = value; }
        }

        public InGameManager InGameManager
        {
            get { return _inGameManager; }
            set { _inGameManager = value; }
        }

        public MenuManager MenuManager
        {
            get { return _menuManager; }
            set { _menuManager = value; }
        }


        public Manager(Game game)
            : base(game)
        {
            _inGameManager = new InGameManager(Game, this);
            _menuManager =  new MenuManager(Game, this);
            _currentGameState = GameState.InMenu;

            Game.Components.Add(_inGameManager);
            Game.Components.Add(_menuManager);
        }
        
    
        public void SwitchState()
        {
            if (_currentGameState == GameState.InGame)
            {
                _currentGameState = GameState.InMenu;
                _oldGameState = GameState.InGame;
            }

            else if (_currentGameState == GameState.InMenu)
            {
                _currentGameState = GameState.InGame;
                _oldGameState = GameState.InMenu;
            }
        }

        public void SwitchState(bool value)
        {
            _inGameManager.Visible = !value;
            _inGameManager.Enabled = !value;
            _menuManager.Visible = value;
            _menuManager.Enabled = value;

          
        }


    }
}
