using WindowsGame1WithPatterns.Classes.Components;
using WindowsGame1WithPatterns.Classes.KeyboardConfiguration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1WithPatterns.Classes.Screens
{
    /// <summary>
    /// Class to handle the GUI and logic of the main menu of the game
    /// </summary>
    class MainMenu : State.State
    {
        /// <summary>
        /// Will handle the menu for the game
        /// </summary>
        private MenuComponent _menuComponent;


        private TextBoxComponent _headline;

        /// <summary>
        /// Placeholder for the background image of the menu
        /// </summary>
        private Texture2D _image;

        /// <summary>
        /// The rectangle to draw the background pucture inside
        /// </summary>
        private Rectangle _imageRectangle;

        /// <summary>
        /// Gets and sets the SelectedIndex property of the MenuComponent
        /// </summary>
        public int SelectedIndex
        {
            get { return _menuComponent.SelectedIndex; }
            set { _menuComponent.SelectedIndex = value; }
        }

        //Constructor...
        public MainMenu(Game game,
            SpriteBatch spriteBatch,
            string managerId,
            SpriteFont menuItemFont,
            SpriteFont headlineFont,
            Texture2D image)
            : base(game, spriteBatch, managerId, GameStates.MainMenu)
        {
            //Create menu item list
            string[] menuItems = { "Start Game", "Highscore", "Options", "Help", "End Game" };
            //Instantiate the MenuComponent
            _menuComponent = new MenuComponent(game,
                spriteBatch,
                menuItemFont,
                menuItems);
            //Add the menu to the components of the main menu screen
            Components.Add(_menuComponent);

            string text = "Main Menu, Motherfucker!";
            _headline = new TextBoxComponent(game, 
                spriteBatch,
                headlineFont, 
                text,
                headlineFont.MeasureString(text).X, 
                50);
            _headline.Position = new Vector2(_headline.Position.X, _menuComponent.Position.Y - _headline.Height - 20);
            Components.Add(_headline);

            //Remember the image pointer for the draw method
            _image = image;
            //Create a rectangle that fills the whole window. This is for
            //drawing the background image
            _imageRectangle = new Rectangle(
                0,
                0,
                Game.Window.ClientBounds.Width,
                Game.Window.ClientBounds.Height);
        }

        /// <summary>
        /// Take care of the switching between screens 
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Update(GameTime gameTime)
        {
            if (InputManager.Instance.IsKeyPressed(Keys.Enter))
            {
                switch (SelectedIndex)
                {
                    case 0:
                        ChangeStateTo(GameStates.ChoosePlayerManager);
                        break;
                    case 1:
                        ChangeStateTo(GameStates.Highscore);
                        break;
                    case 2:
                        ChangeStateTo(GameStates.Options);
                        break;
                    case 3:
                        ChangeStateTo(GameStates.HelpMenu);
                        break;
                    case 4:
                        Game.Exit();
                        break;
                }
                //TODO: Forbedre reset funksjonen for alle states...
                //Reset the menu
                SelectedIndex = 0;
                _headline.Reset();
            }
            base.Update(gameTime);
        }
        
        /// <summary>
        /// Draw the menu
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            _spriteBatch.Draw(_image, _imageRectangle, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
