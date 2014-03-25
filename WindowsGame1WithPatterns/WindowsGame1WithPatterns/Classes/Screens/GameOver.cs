using Jump.Classes.Components;
using Jump.Classes.Highscores;
using Jump.Classes.KeyboardConfiguration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Jump.Classes.Screens
{
    class GameOver : State.State
    {
        /// <summary>
        /// Will handle the menu for the game
        /// </summary>
        private MenuComponent _menuComponent;

        private TextBoxComponent _scoreBoard;
        private TextBoxComponent _headline;
        private List<Score> _newHighscores;

        /// <summary>
        /// Holds the game over tune
        /// </summary>
        private Song _gameOverSong;

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
        public GameOver(Game game,
            SpriteBatch spriteBatch,
            string managerId,
            SpriteFont spriteFont,
            SpriteFont headlineFont,
            Texture2D image)
            : base(game, spriteBatch, managerId, GameStates.GameOver)
        {
            //Add the scoreBoard
            string scoreBoardText = "Player 1: 1000";
            _scoreBoard = new TextBoxComponent(game,
                spriteBatch,
                spriteFont,
                scoreBoardText,
                spriteFont.MeasureString(scoreBoardText).X);
            Components.Add(_scoreBoard);

            //Add a headline
            string headlineText = "Game over!";
            _headline = new TextBoxComponent(game,
                spriteBatch,
                headlineFont,
                headlineText,
                headlineFont.MeasureString(headlineText).X);
            Components.Add(_headline);

            //Create menu item list
            string[] menuItems = { "Back to main menu" };
            //Instantiate the MenuComponent
            _menuComponent = new MenuComponent(game,
                spriteBatch,
                spriteFont,
                menuItems);
            //Add the menu to the components of the main menu screen
            Components.Add(_menuComponent);

            //Fix Positions of the components
            PositionizeGameOverComponents();

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

        public void PrepareGameOverScreen(List<int> scoreList)
        {
            var scoreText = "";
            var playerNumber = 1;
            _newHighscores = new List<Score>();
            
            foreach (var playerScore in scoreList)
            {
                if (Highscore.Instance.IsNewHighscore(playerScore))
                    _newHighscores.Add(new Score(playerScore, "Player " + playerNumber));

                scoreText = string.Concat(scoreText, "Player " + (playerNumber++) + ": " + playerScore + "\n");
            }

            _scoreBoard.Text = scoreText;
            PositionizeGameOverComponents();
        }

        private void PositionizeGameOverComponents()
        {
            var extraSpaceing = 0;
            _scoreBoard.Position = new Vector2(
                (Game.Window.ClientBounds.Width - _scoreBoard.Width) / 2,
                (Game.Window.ClientBounds.Height - _scoreBoard.Height) / 2);
            _headline.Position = new Vector2(
                _headline.Position.X,
                _scoreBoard.Position.Y - _headline.Height - extraSpaceing);
            _menuComponent.Position = new Vector2(
                _menuComponent.Position.X,
                _scoreBoard.Position.Y + _scoreBoard.Height + extraSpaceing);
        }

        protected override void LoadContent()
        {
            _gameOverSong = _game.Content.Load<Song>("Audio/GameOverSong");
            base.LoadContent();
        }

        public override void Show()
        {
            MediaPlayer.IsRepeating = false;
            MediaPlayer.Play(_gameOverSong);



            base.Show();
        }

        public override void Hide()
        {
            MediaPlayer.Stop();
            base.Hide();
        }

        /// <summary>
        /// Take care of the switching between screens 
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Update(GameTime gameTime)
        {
            if (_newHighscores.Count != 0)
            {
                //Prepare new highscores screen and show it.
                ((NewHighscore)GetState(GameStates.NewHighscore)).PrepareNewHighscoreScreen(_newHighscores);
                PopUp(GameStates.NewHighscore);

                //Reset the list when highscore is sendt to NewHighscore screen
                _newHighscores = new List<Score>();
            }

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
            }
            base.Update(gameTime);
        }
        
        /// <summary>
        /// Draw the background
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
