using Jump.Classes.Components;
using Jump.Classes.KeyboardConfiguration;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Jump.Classes.Screens
{
    class InGameMenu : State.State
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
        public InGameMenu(Game game,
            SpriteBatch spriteBatch,
            string managerId,
            SpriteFont spriteFont,
            SpriteFont headlineFont,
            Texture2D image)
            : base(game, spriteBatch, managerId, GameStates.InGameMenu)
        {
            //Create menu item list
            string[] menuItems = {"Resume game", "Exit to main menu"};
            //Instantiate the MenuComponent
            _menuComponent = new MenuComponent(game,
                spriteBatch,
                spriteFont,
                menuItems);
            //Add the menu to the components of the main menu screen
            Components.Add(_menuComponent);

            string text = "Pause";
            _headline = new TextBoxComponent(game,
                spriteBatch,
                headlineFont,
                text,
                headlineFont.MeasureString(text).X,
                0);
            _headline.Position = new Vector2(_headline.Position.X, _menuComponent.Position.Y - _headline.Height - 20);
            Components.Add(_headline);

            //Remember the image pointer for the draw method
            _image = image;
            //Create a rectangle that fills the whole window. This is for
            //drawing the background image
            const int imageWidth = 600;
            const int imageHeight = 400;
            _imageRectangle = new Rectangle(
                (Game.Window.ClientBounds.Width - imageWidth) / 2,
                (Game.Window.ClientBounds.Height - imageHeight) / 2,
                imageWidth,
                imageHeight);
        }

        public override void Show()
        {
            SelectedIndex = 0;
            _headline.Reset();
            base.Show();
        }

        /// <summary>
        /// Take care of the switching between screens 
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Update(GameTime gameTime)
        {
            if (InputManager.Instance.IsKeyPressed(Keys.Escape))
                ChangeStateTo(GameStates.GameManager);

            if (InputManager.Instance.IsKeyPressed(Keys.Enter))
            {
                switch (SelectedIndex)
                {
                    case 0:
                        ChangeStateTo(GameStates.GameManager);
                        break;
                    case 1:
                        ChangeStateTo(GameStates.MainMenu);
                        break;
                }
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
