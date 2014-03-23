using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame1WithPatterns.Classes.Components;
using WindowsGame1WithPatterns.Classes.KeyboardConfiguration;
using Microsoft.Xna.Framework.Input;
using WindowsGame1WithPatterns.Classes.Highscores;

namespace WindowsGame1WithPatterns.Classes.Screens
{
    class NewHighscore : State.State
    {
        private MenuComponent _menuComponent;
        private TextBoxComponent _headline;
        private Texture2D _image;
        private Rectangle _imageRectangle;

        private SpriteFont _spriteFont;

        private const int Inputwidth = 300;

        private int _selectedInputFieldIndex;

        private List<InputField> _inputBoxList;
        private List<Score> _scoreList;

        private struct InputField
        {
            public TextInputComponent InputBox;
            public TextBoxComponent InputPlayer;
        };

        //Constructor
        public NewHighscore(Game game,
            SpriteBatch spriteBatch,
            string managerId,
            SpriteFont spriteFont,
            SpriteFont headlineFont,
            Texture2D image)
            : base(game, spriteBatch, managerId, GameStates.NewHighscore)
        {
            _game = game;
            _spriteFont = spriteFont;

            //Create menu item list
            string[] menuItems = { "Confirm" };
            //Instantiate the MenuComponent
            _menuComponent = new MenuComponent(game,
                spriteBatch,
                spriteFont,
                menuItems);
            //Add the menu to the components of the main menu screen
            Components.Add(_menuComponent);

            //Add a headline
            string text = "New Highscore!";
            _headline = new TextBoxComponent(game, 
                spriteBatch,
                headlineFont, 
                text,
                headlineFont.MeasureString(text).X);
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

        public void PrepareNewHighscoreScreen(List<Score> scoreList)
        {
            _scoreList = scoreList; 
            _inputBoxList = new List<InputField>();
            foreach (var score in scoreList)
            {
                var playerNameText = score.Name;
                var playerName = new TextBoxComponent(_game,
                    _spriteBatch,
                    _spriteFont,
                    playerNameText,
                    _spriteFont.MeasureString(playerNameText).X);
                Components.Add(playerName);

                var playerNameInput = new TextInputComponent(_game,
                    _spriteBatch,
                    _spriteFont,
                    Inputwidth);
                Components.Add(playerNameInput);

                _inputBoxList.Add(new InputField() { InputPlayer = playerName, InputBox = playerNameInput });
            }

            _inputBoxList[0].InputBox.Selected = true;

            //Positionize the components of this screen
            PositionizeNewHighscoreComponents();
        }

        private void PositionizeNewHighscoreComponents()
        {
            //TODO: Flyutte ut?
            const int inputBoxVerticalSpace = 10;
            const int inputBoxHorisontalSpace = 10;

            var inputBoxHeight = _inputBoxList.Count * (_spriteFont.LineSpacing + inputBoxVerticalSpace);

            var verticalOffset = 0;

            foreach (var item in _inputBoxList)
            {
                var inputLineWidth = item.InputPlayer.Width + item.InputBox.Width + inputBoxHorisontalSpace;
                item.InputPlayer.Position = new Vector2(
                    (Game.Window.ClientBounds.Width - inputLineWidth) / 2,
                    ((Game.Window.ClientBounds.Height - inputBoxHeight) / 2) + verticalOffset);

                item.InputBox.Position = new Vector2(
                    item.InputPlayer.Position.X + item.InputPlayer.Width + inputBoxHorisontalSpace,
                    ((Game.Window.ClientBounds.Height - inputBoxHeight) / 2) + verticalOffset);

                verticalOffset += _spriteFont.LineSpacing + inputBoxVerticalSpace;
            }

            _headline.Position = new Vector2(
                _headline.Position.X,
                ((Game.Window.ClientBounds.Height - inputBoxHeight) / 2f) - _headline.Height - inputBoxVerticalSpace);
            _menuComponent.Position = new Vector2(
                _menuComponent.Position.X,
                ((Game.Window.ClientBounds.Height - inputBoxHeight) / 2f) + inputBoxHeight + inputBoxVerticalSpace);
        }
        

        /// <summary>
        /// Take care of the switching between screens 
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Update(GameTime gameTime)
        {
            //Change the selected input field
            if (InputManager.Instance.IsKeyPressed(Keys.Tab) && !InputManager.Instance.IsKeyPressed(Keys.RightShift))
            {
                _inputBoxList[_selectedInputFieldIndex].InputBox.Selected = false;
                if ((++_selectedInputFieldIndex) >= _inputBoxList.Count)
                    _selectedInputFieldIndex = 0;
                _inputBoxList[_selectedInputFieldIndex].InputBox.Selected = true;
            }
            if (InputManager.Instance.IsKeyPressed(Keys.Tab) && InputManager.Instance.IsKeyPressed(Keys.RightShift))
            {
                _inputBoxList[_selectedInputFieldIndex].InputBox.Selected = false;
                if ((--_selectedInputFieldIndex) < 0)
                    _selectedInputFieldIndex = _inputBoxList.Count - 1;
                _inputBoxList[_selectedInputFieldIndex].InputBox.Selected = true;
            }

            if (InputManager.Instance.IsKeyPressed(Keys.Enter))
                switch (_menuComponent.SelectedIndex)
                {
                    case 0:
                        for (var i = 0; i < _scoreList.Count; i++)
                        {
                            _scoreList[i].Name = _inputBoxList[i].InputBox.Text;
                            Highscore.Instance.AddScore(_scoreList[i]);
                        }
                        ChangeStateTo(GameStates.GameOver);
                        break;
                }
            base.Update(gameTime);
        }

        public override void Hide()
        {
            if (_inputBoxList != null)
                foreach (var inputBox in _inputBoxList)
                {
                    Components.Remove(inputBox.InputBox);
                    Components.Remove(inputBox.InputPlayer);
                }
            
            base.Hide();
        }

        /// <summary>
        /// Draw the image
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
