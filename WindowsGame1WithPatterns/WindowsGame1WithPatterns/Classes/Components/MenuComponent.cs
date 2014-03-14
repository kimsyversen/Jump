using WindowsGame1WithPatterns.Classes.KeyboardConfiguration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1WithPatterns.Classes.Components
{
    /// <summary>
    /// Class that takes care of a list of choises, highlights the choise the player has selected
    /// and expose the choise to other classes. Also known as a menu...
    /// </summary>
    public class MenuComponent : DrawableGameComponent
    {
        /// <summary>
        /// Will hold the items for the menu
        /// </summary>
        private string[] _menuItems;

        /// <summary>
        /// Will hold the index fot the item currently selected
        /// </summary>
        private int _selectedIndex;

        /// <summary>
        /// Color to draw the normal menu items
        /// </summary>
        private Color _normal = Color.White;

        /// <summary>
        /// Color to draw the currently selected menu item
        /// </summary>
        private Color _hilite = Color.Yellow;

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
        /// The spaceing between each menu item
        /// </summary>
        private float _menuItemSpaceing;

        /// <summary>
        /// Property for selectedIndex variable with validation
        /// checking on the set. 
        /// </summary>
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                if (_selectedIndex < 0)
                    _selectedIndex = 0;
                if (_selectedIndex >= _menuItems.Length)
                    _selectedIndex = _menuItems.Length - 1;
            }
        }

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

        //Constructor...
        public MenuComponent(Game game,
            SpriteBatch spriteBatch,
            SpriteFont spriteFont,
            string[] menuItems)
            : base(game)
        {
            _normal = Color.White;
            _hilite = Color.Yellow;
            _menuItemSpaceing = 0f;
            _spriteBatch = spriteBatch;
            _spriteFont = spriteFont;
            _menuItems = menuItems;
            MeasureMenu();
        }

        /// <summary>
        /// Mesure menu to position it on the center of the screen
        /// </summary>
        private void MeasureMenu()
        {
            //Reset the height and width
            _height = 0;
            _width = 0;
            foreach (string item in _menuItems)
            {
                Vector2 size = _spriteFont.MeasureString(item);
                if (size.X > _width)
                    _width = size.X;

                _height += _spriteFont.LineSpacing + _menuItemSpaceing;
            }

            _position = new Vector2(
                (Game.Window.ClientBounds.Width - _width) / 2,
                (Game.Window.ClientBounds.Height - _height) / 2);
        }

        /// <summary>
        /// Reset the component
        /// </summary>
        public void Reset()
        {
            _selectedIndex = 0;
        }

        /// <summary>
        /// Update the position of the selected menu item
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Update(GameTime gameTime)
        {
            //If the down key is pressed, move selected index down the
            //menu by increesing the index. If the last menu item is 
            //selected when down key is pressed, the next selected item
            //will be the first in the list. The oposite description holds
            //for the second if statement
            if (InputManager.Instance.IsKeyPressed(Keys.Down))
            {
                _selectedIndex++;
                if (_selectedIndex == _menuItems.Length)
                    _selectedIndex = 0;
            }
            if (InputManager.Instance.IsKeyPressed(Keys.Up))
            {
                _selectedIndex--;
                if (_selectedIndex < 0)
                    _selectedIndex = _menuItems.Length - 1;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw the menu
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Draw(GameTime gameTime)
        {
            //Draw the base first so that the menu will be drawn on top of everything else
            base.Draw(gameTime);

            //Will hold where to draw the next line of the menu and is set 
            //initially to the position field that was calculated in MeasureMenu
            Vector2 location = _position;
            //Will be used to determine what color to draw the text
            Color tint;
            
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            //Loops through all of the menu items check if the index, i, is equal to selectedIndex. 
            for (int i = 0; i < _menuItems.Length; i++)
            {
                //If i is equal to selectedIndex: set tint to hilite so the item will be hilited.
                //Other wise: set tint to normal because it is regular text.
                if (i == _selectedIndex)
                    tint = _hilite;
                else
                    tint = _normal;

                //Draw the string
                _spriteBatch.DrawString(
                    _spriteFont,
                    _menuItems[i],
                    location,
                    tint);

                //Set the Y location for the next menu item to be drawn
                location.Y += _spriteFont.LineSpacing + _menuItemSpaceing;
            }
            _spriteBatch.End();
        }
    }
}
