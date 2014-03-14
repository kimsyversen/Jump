using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.Components
{
    class TextBoxComponent : DrawableGameComponent
    {
        /// <summary>
        /// Color of the text
        /// </summary>
        private Color _textColor;

        /// <summary>
        /// Will be used for drawing the text of the menu
        /// </summary>
        private SpriteBatch _spriteBatch;

        /// <summary>
        /// Will be used to position the menu on the screen
        /// </summary>
        private Vector2 _position;

        /// <summary>
        /// Will hold the calculated width of the menu
        /// </summary>
        private float _width = 0f;

        /// <summary>
        /// Will hold the calculated hight of the menu
        /// </summary>
        private float _height = 0f;

        /// <summary>
        /// Sprite front for the text of the menu items
        /// </summary>
        private SpriteFont _spriteFont;

        /// <summary>
        /// Will hold the parsed text that is to be desplayed
        /// in the textBox
        /// </summary>
        private string _parsedText;

        /// <summary>
        /// Will hold the text typed so far if the textBox shuld
        /// type it, that is if (_delayInMilliseconds > 0)
        /// </summary>
        private string _typedText;

        /// <summary>
        /// Will hold the length of the string typed so far
        /// </summary>
        private double _typedTextLength;

        /// <summary>
        /// Will hold the delay of each character typeing
        /// if (0 > _delayInMilliseconds) the typing effect will
        /// be diabled.
        /// </summary>
        private int _delayInMilliseconds;

        /// <summary>
        /// True if the textBox is done drawing the text
        /// else false.
        /// </summary>
        private bool _isDoneDrawing;

        /// <summary>
        /// Get or set the position of the menu
        /// </summary>
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        /// <summary>
        /// Get property for the width variable
        /// </summary>
        public float Width
        {
            get { return _width; }
        }

        /// <summary>
        /// Get property for the height variable
        /// </summary>
        public float Height
        {
            get { return _height; }
        }

        //Constructor with the typing daly
        public TextBoxComponent(Game game, 
            SpriteBatch spriteBatch,
            SpriteFont spriteFont,
            string text,
            float width,
            int characterTypingDelay) 
            : base(game)
        {
            _spriteBatch = spriteBatch;
            _spriteFont = spriteFont;
            _width = width;
            _textColor = Color.White;
            _parsedText = parseText(text);
            _delayInMilliseconds = characterTypingDelay;
            _isDoneDrawing = false;
        }

        //Constructor without the typing daly
        public TextBoxComponent(Game game,
            SpriteBatch spriteBatch,
            SpriteFont spriteFont,
            string text,
            float width)
            : this(game, spriteBatch, spriteFont, text, width, 0)
        {
        }

        /// <summary>
        /// Parse the string to fit it into the _width specified trough
        /// the constructor, and call the MeasureTextBox to calculate 
        /// the hight and position.
        /// </summary>
        /// <param name="text">The string to be parsed to fit inside the _width</param>
        /// <returns>Returns the parsed string</returns>
        private string parseText(string text)
        {
            string line = string.Empty;
            string returnString = string.Empty;
            string[] wordArray = text.Split(' ');
            int lineNumbers = 1;

            foreach (string word in wordArray)
            {
                if (_spriteFont.MeasureString(line + word).X > _width)
                {
                    returnString = returnString + line + '\n';
                    line = string.Empty;
                    lineNumbers++;
                }

                line = line + word + ' ';
            }
            MeasureTextBox(lineNumbers);
            return returnString + line;
        }

        /// <summary>
        /// Measure the height of the textBox and calculate the position
        /// so that the text is default on the center of the screen.
        /// </summary>
        /// <param name="lineNumbers">The number of lines that the textbox consist of</param>
        private void MeasureTextBox(int lineNumbers)
        {
            _height = _spriteFont.LineSpacing * lineNumbers;
            _position = new Vector2(
                (Game.Window.ClientBounds.Width - _width) / 2,
                (Game.Window.ClientBounds.Height - _height) / 2);
        }

        /// <summary>
        /// Update the typing for the textbox
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Update(GameTime gameTime)
        {
            if (!_isDoneDrawing)
            {
                if (_delayInMilliseconds <= 0)
                {
                    _typedText = _parsedText;
                    _isDoneDrawing = true;
                }
                else if (_typedTextLength < _parsedText.Length)
                {
                    _typedTextLength = _typedTextLength + gameTime.ElapsedGameTime.TotalMilliseconds / _delayInMilliseconds;

                    if (_typedTextLength >= _parsedText.Length)
                    {
                        _typedTextLength = _parsedText.Length;
                        _isDoneDrawing = true;
                    }
                    _typedText = _parsedText.Substring(0, (int)_typedTextLength);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw the text
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            _spriteBatch.DrawString(_spriteFont, _typedText, _position, Color.White);
            _spriteBatch.End();
        }

        /// <summary>
        /// Reset the component
        /// </summary>
        public void Reset()
        {
            _typedText = "";
            _typedTextLength = 0;
            _isDoneDrawing = false;
        }
    }
}