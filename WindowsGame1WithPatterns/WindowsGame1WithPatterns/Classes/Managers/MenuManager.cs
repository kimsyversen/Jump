using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WindowsGame1WithPatterns.Classes.Sprites.Factories;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts;

namespace WindowsGame1WithPatterns.Classes.Managers
{
    class MenuManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteFactory _spriteFactory;

        SpriteBatch _spriteBatch;
        private FontFactory _fontFactory;

        private List<SpriteFont> _fonts;

        private SpriteFont _newGameFont;
        private SpriteFont _exitFont;

        public int SelectedIndex = 0;

        private KeyboardState _oldKeyState;
        private KeyboardState _newKeyState;

        public MenuManager(Game game) : base(game)
        {
        }

        public override void Initialize()
        {   
            _spriteFactory = new SpriteFactory(Game);
            _fonts = new List<SpriteFont>();
            _newGameFont = Game.Content.Load<SpriteFont>("Menu/NewGame");
            _exitFont = Game.Content.Load<SpriteFont>("Menu/Exit");

            _fonts.Add(_newGameFont);
            _fonts.Add(_exitFont);

            base.Initialize(); 
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
                Game.Exit();


            //Need to to this because updating of frames happens faster than a user releases a key
            //Without, it would just switch fast between menu and game
            _newKeyState = Keyboard.GetState();

            if (SelectedIndex < _fonts.Count && SelectedIndex >= 0)
            {
                


                if (_newKeyState.IsKeyDown(Keys.Down) && !_oldKeyState.IsKeyDown(Keys.Down))
                    // If not down last update, key has just been pressed.
                    if (!_oldKeyState.IsKeyDown(Keys.Down))
                        Console.WriteLine("Pressed key down");
                    else
                        // Key was down last update, but not down now, so
                        // it has just been released.
                        // the player just pressed down
                        SelectedIndex++;

                if (_newKeyState.IsKeyDown(Keys.Up) && !_oldKeyState.IsKeyDown(Keys.Up))
                    // If not down last update, key has just been pressed.
                    if (!_oldKeyState.IsKeyDown(Keys.Up))
                        Console.WriteLine("Pressed key up");
                    else
                        // Key was down last update, but not down now, so
                        // it has just been released.
                        // the player just pressed down
                        SelectedIndex--;

            }
     

            //Change color on font with selectedIndex
               
           

            //Store the old state
            _oldKeyState = _newKeyState;
        

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AntiqueWhite);

            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            _spriteBatch.DrawString(_newGameFont, "New Game", new Vector2(Game.Window.ClientBounds.Width / 2f, Game.Window.ClientBounds.Height / 4f) , Color.Red);
            _spriteBatch.DrawString(_newGameFont, "Exit", new Vector2(Game.Window.ClientBounds.Width / 2f, Game.Window.ClientBounds.Height / 3f), Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }


    }
}
