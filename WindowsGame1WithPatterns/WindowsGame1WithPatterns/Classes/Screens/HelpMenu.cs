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
    class HelpMenu : State.State
    {
        /// <summary>
        /// Will handle the menu for the game
        /// </summary>
        private MenuComponent _menuComponent;

        private TextBoxComponent _textBoxComponent;

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
        public HelpMenu(Game game,
            SpriteBatch spriteBatch,
            string managerId,
            SpriteFont spriteFont,
            Texture2D image)
            : base(game, spriteBatch, managerId, GameStates.HelpMenu)
        {
            //Create menu item list
            string[] menuItems = { "Back to main menu"};
            //Instantiate the MenuComponent
            _menuComponent = new MenuComponent(game,
                spriteBatch,
                spriteFont,
                menuItems);
            //Add the menu to the components of the main menu screen
            Components.Add(_menuComponent);

            string text = "This is how you play:\n" + 
                "This is a long string to test if the text goes out of the bounds... So I do sure hope it is long enough!";
            _textBoxComponent = new TextBoxComponent(game,
                spriteBatch,
                spriteFont,
                text,
                game.Window.ClientBounds.Width - 200,
                10);
            Components.Add(_textBoxComponent);

            //position the components on the screen
            _menuComponent.Position = new Vector2(_menuComponent.Position.X, _textBoxComponent.Position.Y + _textBoxComponent.Height + _menuComponent.Height + 20);
            //_textBoxComponent.position = new Vector2(_textBoxComponent.position.X, _menuComponent.position.Y - _textBoxComponent.Height - 20);

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
                        ChangeStateTo(GameStates.MainMenu);
                        break;
                }
                //Reset the menu
                SelectedIndex = 0;
                _textBoxComponent.Reset();
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
