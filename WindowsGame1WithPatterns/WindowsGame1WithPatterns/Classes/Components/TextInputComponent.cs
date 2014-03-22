using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame1WithPatterns.Classes.KeyboardConfiguration;

namespace WindowsGame1WithPatterns.Classes.Components
{
    class TextInputComponent : DrawableGameComponent
    {
        /// <summary>
        /// Text in the text component
        /// </summary>
        private string _text;

        /// <summary>
        /// Position of the text component
        /// </summary>
        private Vector2 _position;

        /// <summary>
        /// Width of the text component
        /// </summary>
        private float _width;

        /// <summary>
        /// Height of the text component
        /// </summary>
        private float _height;

        /// <summary>
        /// Will be used to be drawn with
        /// </summary>
        private SpriteBatch _spriteBatch;

        /// <summary>
        /// The font to draw the text with
        /// </summary>
        private SpriteFont _spriteFont;

        /// <summary>
        /// The color of the text
        /// </summary>
        private Color _textColor;

        /// <summary>
        /// Get or set the text of the text component
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                //If the text is smaler than the width of the text component,
                //the text can be set, if not the string must be shortened before
                //beeing set 
                if (_spriteFont.MeasureString(value).X < _width)
                    _text = value;
                else
                {
                    string s = value;
                    while (_spriteFont.MeasureString(s).X >= _width)
                        s = s.Remove(s.Length - 1);
                    _text = s;
                }
            }
        }

        public bool Selected { get; set; }

        /// <summary>
        /// Get or set the text color of the text component
        /// </summary>
        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; }
        }

        /// <summary>
        /// Get or set the position of the text component
        /// </summary>
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        /// <summary>
        /// Get the height of the text component
        /// </summary>
        public float Height
        {
            get { return _height; }
        }

        /// <summary>
        /// Get the width of the text component
        /// </summary>
        public float Width
        {
            get { return _width; }
        }


        #region Ctor
        public TextInputComponent(
            Game game,
            SpriteBatch spriteBatch,
            SpriteFont spriteFont,
            float width,
            string text,
            Vector2 position,
            Color textColor)
            : base(game)
        {
            _spriteBatch = spriteBatch;
            _spriteFont = spriteFont;
            _width = width;
            _text = text;
            _position = position;
            _textColor = textColor;
            _height = _spriteFont.LineSpacing;
            Selected = false;
        }
        public TextInputComponent(
            Game game,
            SpriteBatch spriteBatch,
            SpriteFont spriteFont,
            float width,
            string text,
            Vector2 position)
            : this(game, spriteBatch, spriteFont, width, text, position, Color.White) { }
        public TextInputComponent(
            Game game,
            SpriteBatch spriteBatch,
            SpriteFont spriteFont,
            float width,
            string text)
            : this(game, spriteBatch, spriteFont, width, text, Vector2.Zero) { }
        public TextInputComponent(
            Game game,
            SpriteBatch spriteBatch,
            SpriteFont spriteFont,
            float width)
            : this(game, spriteBatch, spriteFont, width, "") { }
        #endregion



        /// <summary>
        /// Calculate and set the position of the textbox in the center of the screen.
        /// </summary>
        public void Center()
        {
            _position = new Vector2(
                (Game.Window.ClientBounds.Width - _width) / 2,
                (Game.Window.ClientBounds.Height - _height) / 2);
        }

        /// <summary>
        /// Draw the text
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            _spriteBatch.DrawString(_spriteFont, _text, _position, _textColor);
            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (Selected)
                Text = InputManager.Instance.KeyboardStream(Text);

            base.Update(gameTime);
        }

        /// <summary>
        /// Reset the component
        /// </summary>
        public void Reset()
        {
            Text = "";
        }
    }
}
