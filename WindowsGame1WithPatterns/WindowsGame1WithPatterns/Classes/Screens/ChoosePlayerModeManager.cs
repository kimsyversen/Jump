﻿using Jump.Classes.Components;
using Jump.Classes.KeyboardConfiguration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Jump.Classes.Screens
{
    class ChoosePlayerModeManager : State.State
    {
        /// <summary>
        /// Will handle the menu for the game
        /// </summary>
        private MenuComponent _menuComponent;

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
        public ChoosePlayerModeManager(Game game,
            SpriteBatch spriteBatch,
            string managerId,
            SpriteFont spriteFont,
            Texture2D image)
            : base(game, spriteBatch, managerId, GameStates.ChoosePlayerModeManager)
        {
            //Create menu item list
            string[] menuItems = { "Single player", "Multiplayer (two player)", "Back to main menu"};
            //Instantiate the MenuComponent
            _menuComponent = new MenuComponent(game,
                spriteBatch,
                spriteFont,
                menuItems);
            //Add the menu to the components of the main menu screen
            Components.Add(_menuComponent);
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

        /// <summary>
        /// Take care of the switching between screens 
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Update(GameTime gameTime)
        {
            if (InputManager.Instance.IsKeyPressed(Keys.Escape))
            {
                ChangeStateTo(GameStates.MainMenu);
                //Reset the menu
                SelectedIndex = 0;
            }

            if (InputManager.Instance.IsKeyPressed(Keys.Enter))
            {
                switch (SelectedIndex)
                {
                    case 0:
                        ((GameManager)GetState(GameStates.GameManager)).NumberOfPlayers(1);
                        ChangeStateTo(GameStates.GameManager);
                        break;
                    case 1:
                        ((GameManager)GetState(GameStates.GameManager)).NumberOfPlayers(2);
                        ChangeStateTo(GameStates.GameManager);
                        break;
                    case 2:
                        ChangeStateTo(GameStates.MainMenu);
                        break;
                }
                //Reset the menu
                SelectedIndex = 0;
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
