using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace ScreenManager
{
    public class MenuComponent : DrawableGameComponent
    {
        //Will hold the items for the menu and which 
        //item is currently selected
        private string[] menuItems;
        private int selectedIndex;

        //Color to draw the normal menu items
        private Color normal = Color.White;
        //Color to draw the currently selected menu item
        private Color hilite = Color.Yellow;

        //Will hold the current state of the keyboard and the 
        //state of the keyboard in the previous frame of the game
        private KeyboardState keyboardState;
        private KeyboardState oldKeyboardState;

        //Will be used for drawing the text of the menu
        private SpriteBatch _spriteBatch;
        private SpriteFont spriteFont;

        //Will be used to position the menu on the screen
        private Vector2 position;
        private float width = 0f;
        private float height = 0f;
        private float menuItemSpaceing = 0f;

        //Property for selectedIndex variable with validation
        //checking on the set. 
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                if (selectedIndex < 0)
                    selectedIndex = 0;
                if (selectedIndex >= menuItems.Length)
                    selectedIndex = menuItems.Length - 1;
            }
        }

        //Property for position variable
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        //Get property for the width variable
        public float Width
        {
            get { return width; }
        }

        //Get property for the height variable
        public float Height
        {
            get { return height; }
        }

        //Constructor...
        public MenuComponent(Game game,
            SpriteBatch spriteBatch,
            SpriteFont spriteFont,
            string[] menuItems)
            : base(game)
        {
            this._spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;
            this.menuItems = menuItems;
            MeasureMenu();
        }

        //Mesure menu to position it on the center of the screen
        private void MeasureMenu()
        {
            //Reset the height and width
            height = 0;
            width = 0;
            foreach (string item in menuItems)
            {
                Vector2 size = spriteFont.MeasureString(item);
                if (size.X > width)
                    width = size.X;

                height += spriteFont.LineSpacing + menuItemSpaceing;
            }

            position = new Vector2(
                (Game.Window.ClientBounds.Width - width) / 2,
                (Game.Window.ClientBounds.Height - height) / 2);
        }

        //Check if theKey is pressed between frames
        //True: it is
        //False: it is not
        private bool CheckKey(Keys theKey)
        {
            return keyboardState.IsKeyUp(theKey) &&
                oldKeyboardState.IsKeyDown(theKey);
        }

        public override void Update(GameTime gameTime)
        {
            //Get the keyboard state
            keyboardState = Keyboard.GetState();

            //If the key is pressed down, move selected index down the
            //menu by increesing the index. If the last menu item is 
            //selected when down key is pressed, the next selected item
            //will be the first in the list. The oposite description holds
            //for the second if statement
            if (CheckKey(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Length)
                    selectedIndex = 0;
            }
            if (CheckKey(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = menuItems.Length - 1;
            }

            base.Update(gameTime);

            //Remember the old keyboard state for the next iteration
            oldKeyboardState = keyboardState;
        }

        public override void Draw(GameTime gameTime)
        {
            //Draw the base first so that the menu will be drawn on top of everything else
            base.Draw(gameTime);

            //Will hold where to draw the next line of the menu and is set 
            //initially to the position field that was calculated in MeasureMenu
            Vector2 location = position;
            //Will be used to determine what color to draw the text
            Color tint;
            
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            //Loops through all of the menu items check if the index, i, is equal to selectedIndex. 
            for (int i = 0; i < menuItems.Length; i++)
            {
                //If i is equal to selectedIndex: set tint to hilite so the item will be hilited.
                //Other wise: set tint to normal because it is regular text.
                if (i == selectedIndex)
                    tint = hilite;
                else
                    tint = normal;

                //Draw the string
                _spriteBatch.DrawString(
                    spriteFont,
                    menuItems[i],
                    location,
                    tint);

                //Set the Y location for the next menu item to be drawn
                location.Y += spriteFont.LineSpacing + menuItemSpaceing;
            }
            _spriteBatch.End();
        }
    }
}

