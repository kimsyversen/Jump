using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame1WithPatterns.Classes.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WindowsGame1WithPatterns.Classes.KeyboardConfiguration;
using Microsoft.Xna.Framework.Input;
using WindowsGame1WithPatterns.Classes.Highscores;

namespace WindowsGame1WithPatterns.Classes.Screens
{
    class HighscoreMenu : State.State
    {
        /// <summary>
        /// Will handle the menu for the game
        /// </summary>
        private MenuComponent _menuComponent;

        /// <summary>
        /// Will hold the headline for the screen
        /// </summary>
        private TextBoxComponent _headline;

        /// <summary>
        /// Will hold the highscore content for the screen
        /// </summary>
        private TextBoxComponent _highscoreContent;

        /// <summary>
        /// Used to get highscores
        /// </summary>
        private Highscore highscore = Highscore.Instance;

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

        //Constructor
        public HighscoreMenu(Game game,
            SpriteBatch spriteBatch,
            string managerId,
            SpriteFont spriteFont,
            SpriteFont headlineFont,
            Texture2D image)
            : base(game, spriteBatch, managerId, GameStates.Highscore)
        {
            //Create menu item list
            string[] menuItems = { "Back to main menu" };
            //Instantiate the MenuComponent
            _menuComponent = new MenuComponent(game,
                spriteBatch,
                spriteFont,
                menuItems);
            //Add the menu to the components of the main menu screen
            Components.Add(_menuComponent);

            //Add a headline
            string text = "Highscore";
            _headline = new TextBoxComponent(game,
                spriteBatch,
                headlineFont,
                text,
                headlineFont.MeasureString(text).X);
            Components.Add(_headline);

            //Add a headline
            string highscoreContentText = "1) Magnus Sandgren : 10000";
            _highscoreContent = new TextBoxComponent(game,
                spriteBatch,
                spriteFont,
                highscoreContentText,
                headlineFont.MeasureString(highscoreContentText).X);
            Components.Add(_highscoreContent);

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

        public void PrepareHighscoreMenu()
        {
            var highscoreText = "";
            var highscorePosition = 1;

            foreach (var highscore in Highscore.Instance.Highscores.OrderByDescending(o => o.Points).ToList())
            {
                highscoreText = string.Concat(highscoreText, (highscorePosition++) + ") " + highscore.Name + " : " + highscore.Points + "\n");
            }
            _highscoreContent.Text = highscoreText;
            PositionizeHighscoreComponents();
        }

        private void PositionizeHighscoreComponents()
        {
            var extraSpaceing = 0;
            _highscoreContent.Position = new Vector2(
                (Game.Window.ClientBounds.Width - _highscoreContent.Width) / 2,
                (Game.Window.ClientBounds.Height - _highscoreContent.Height) / 2);
            _headline.Position = new Vector2(
                _headline.Position.X,
                _highscoreContent.Position.Y - _headline.Height - extraSpaceing);
            _menuComponent.Position = new Vector2(
                _menuComponent.Position.X,
                _highscoreContent.Position.Y + _highscoreContent.Height + extraSpaceing);
        }

        public override void Show()
        {
            PrepareHighscoreMenu();
            base.Show();
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
            }

            if (InputManager.Instance.IsKeyPressed(Keys.Escape))
                ChangeStateTo(GameStates.MainMenu);

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
