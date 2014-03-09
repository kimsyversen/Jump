using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WindowsGame1WithPatterns.Classes.KeyboardConfiguration;

namespace WindowsGame1WithPatterns.Classes.Managers.Magnus
{
    class MainManager : DrawableGameComponent
    {
        protected StateManager CurrentStateManager;
        public int GameInProgress { get; set; }

        private StateManager _menuManager;
        private StateManager _gameManager;

        public MainManager(Game game) : base(game){}

        /// <summary>
        /// Set values that shall be initialized here
        /// </summary>
        public override void Initialize()
        {
            _menuManager = new MenuManager(Game);
            _gameManager = new GameManager(Game);

            //Start the game in the menu
            CurrentStateManager = _menuManager;

            //Since all managers are default off, enable the one that shall be started
            CurrentStateManager.Enable(true);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            
        }

   
        private void CheckForNewState()
        {
            var newState = CurrentStateManager.ChangeState();

            if (newState != null)
            {
                //Disable other components
                CurrentStateManager.Enable(false);
                CurrentStateManager = newState;
                //Enable the right component
                CurrentStateManager.Enable(true);
            }
        }

        public override void Update(GameTime gameTime)
        {
            CheckForNewState();
            base.Update(gameTime);
        }
    }
}
