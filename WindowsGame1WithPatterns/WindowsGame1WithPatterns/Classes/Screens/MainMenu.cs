using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScreenManager;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WindowsGame1WithPatterns.Classes.States
{
    class MainMenu : State
    {
        //Will handle the menu for the game
        MenuComponent menuComponent;
        //Will be used to fill the entire game window with a background picture
        Texture2D image;
        Rectangle imageRectangle;

        //Gets and sets the SelectedIndex property of the MenuComponent
        public int SelectedIndex
        {
            get { return menuComponent.SelectedIndex; }
            set { menuComponent.SelectedIndex = value; }
        }

        //Constructor...
        public MainMenu(Game game,
            SpriteBatch spriteBatch,
            string managerId,
            SpriteFont spriteFont,
            Texture2D image)
            : base(game, spriteBatch, managerId, GameStates.MainMenu)
        {
            //Create a new menu with two options
            string[] menuItems = { "Start Game", "Highscore", "Options", "End Game" };
            menuComponent = new MenuComponent(game,
                spriteBatch,
                spriteFont,
                menuItems);
            //Add the menu to the components of the screen
            Components.Add(menuComponent);
            //Prepare the image parameters
            this.image = image;
            //Create an rectangle that fills the whole window. This is for
            //drawing the background image
            imageRectangle = new Rectangle(
                0,
                0,
                Game.Window.ClientBounds.Width,
                Game.Window.ClientBounds.Height);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            _spriteBatch.Draw(image, imageRectangle, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
