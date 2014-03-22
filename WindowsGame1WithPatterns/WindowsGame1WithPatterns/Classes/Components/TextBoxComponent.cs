using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.Components
{
    //Sorce: http://danieltian.wordpress.com/2008/12/24/xna-tutorial-typewriter-text-box-with-proper-word-wrapping-part-1/
    class TextBoxComponent : DrawableGameComponent
    {
        #region Members
        /// <summary>
        /// color of the text
        /// </summary>
        private Color _textColor;

        /// <summary>
        /// Will be used for drawing the text of the menu
        /// </summary>
        private SpriteBatch _spriteBatch;

        /// <summary>
        /// Sprite front for the text of the menu items
        /// </summary>
        private SpriteFont _spriteFont;

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
        /// Will hold the raw text string inputed to this component
        /// </summary>
        private string _rawText;

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
        #endregion

        #region Properties
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

        /// <summary>
        /// Get or set the text of the textbox
        /// </summary>
        public string Text
        {
            get { return _rawText; }
            set
            {
                _rawText = value;
                _parsedText = ParseText(_rawText);
                _height = MeasureTextBoxHeight(_parsedText);
                Reset();
            }
        }
        #endregion

        #region Ctor
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
            Text = text;
            _position = Center();
            _delayInMilliseconds = characterTypingDelay;
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
        #endregion

        #region Methods
        /// <summary>
        /// Parse the string to fit it into the _width specified trough
        /// the constructor, and call the MeasureTextBoxHeight to calculate 
        /// the hight.
        /// </summary>
        /// <param name="text">The string to be parsed to fit inside the _width</param>
        /// <returns>Returns the parsed string</returns>
        private string ParseText(string text)
        {
            string line = string.Empty;
            string returnString = string.Empty;
            string[] wordArray = text.Split(' ');

            foreach (string word in wordArray)
            {
                if (_spriteFont.MeasureString(line + word).X > _width)
                {
                    returnString = returnString + line + '\n';
                    line = string.Empty;
                }

                line = line + word + ' ';
            }
            
            return returnString + line;
        }

        /// <summary>
        /// Messure the height of the text box by finding the number of lines of the
        /// parsed text and miltiplying it with the vertical space (LineSpacing) of
        /// the font
        /// </summary>
        /// <param name="parsedText">The text to find the height of</param>
        /// <returns>The height of the textbox with the specified parsed text</returns>
        private float MeasureTextBoxHeight(string parsedText)
        {
            var numberOfLines = parsedText.Split('\n').Length;
            return _spriteFont.LineSpacing * numberOfLines;
            
        }

        /// <summary>
        /// Calculate the center of the textBox
        /// </summary>
        /// <returns>Position that the text box must have to be centered on the screen</returns>
        private Vector2 Center()
        {
            return new Vector2(
                (Game.Window.ClientBounds.Width - _width) / 2,
                (Game.Window.ClientBounds.Height - _height) / 2);
        }

        /// <summary>
        /// Reset the typing of the component
        /// </summary>
        public void Reset()
        {
            _typedText = "";
            _typedTextLength = 0;
            _isDoneDrawing = false;
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
        #endregion
    }
}