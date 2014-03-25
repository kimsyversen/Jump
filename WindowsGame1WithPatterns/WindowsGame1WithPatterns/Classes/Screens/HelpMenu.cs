using Jump.Classes.Components;
using Jump.Classes.KeyboardConfiguration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Jump.Classes.Screens
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

        /// <summary>
        /// Will hold the context of the help menu
        /// </summary>
        private TextBoxComponent _textBoxComponent;

        /// <summary>
        /// Will hold the headline of the help menu
        /// </summary>
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

        public HelpMenu(Game game,
            SpriteBatch spriteBatch,
            string managerId,
            SpriteFont spriteFont,
            SpriteFont headlineFont,
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

            //Add a headline
            string headlineText = "How to play";
            _headline = new TextBoxComponent(game,
                spriteBatch,
                headlineFont,
                headlineText,
                headlineFont.MeasureString(headlineText).X);
            _headline.Position = new Vector2(_headline.Position.X, _menuComponent.Position.Y - _headline.Height - 20);
            Components.Add(_headline);

            string text = "Mission:\n" +
                "Jump as high as you can. As you progress towards the sky, the difficulty will increase. Tip: you can bounce of the wall for that extra boost if your surroundings allow it. Good luck! \n\n" +
                "Controls:\n" +
                "Player one: W to jump, A to go left and D to go right.\n" +
                "Player two: Up key to jump, left key to go left and right key to go right.";
            _textBoxComponent = new TextBoxComponent(game,
                spriteBatch,
                spriteFont,
                text,
                game.Window.ClientBounds.Width - 200,
                10);
            Components.Add(_textBoxComponent);

            const int componentOffest = 20;

            //position the components on the screen
            _menuComponent.Position = new Vector2(_menuComponent.Position.X, _textBoxComponent.Position.Y + _textBoxComponent.Height + _menuComponent.Height + componentOffest);
            _headline.Position = new Vector2(_headline.Position.X, _textBoxComponent.Position.Y - _headline.Height - componentOffest);

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
            if (InputManager.Instance.IsKeyPressed(Keys.Escape))
                ChangeStateTo(GameStates.MainMenu);

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
