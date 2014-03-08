using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WindowsGame1WithPatterns.Classes.Sprites.Factories;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts.Concretes.MenuFonts;

namespace WindowsGame1WithPatterns.Classes.Managers
{
    class InMenuManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteFactory _spriteFactory;

        SpriteBatch _spriteBatch;
        private List<SimpleFont> _fonts;

        private SimpleFont _newGameFont;
        private SimpleFont _exitFont;

        public int SelectedIndex = 0;

        private KeyboardState _oldKeyState;
        private KeyboardState _newKeyState;

        private Manager _manager;

        public InMenuManager(Game game, Manager manager)
            : base(game)
        {
            _manager = manager;
        }

        public override void Initialize()
        {   
            _fonts = new List<SimpleFont>();
            _spriteFactory = new SpriteFactory(Game);

            var fontFactory = _spriteFactory.CreateFontFactory();

            _newGameFont = fontFactory.MenuNewGameFont();
            _exitFont = fontFactory.ExitGameFont();

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

            //Stay inside the indexes that exist in the list
            if (SelectedIndex < _fonts.Count && SelectedIndex >= 0)
            {
                if (_newKeyState.IsKeyDown(Keys.Down) && !_oldKeyState.IsKeyDown(Keys.Down))
                    // Make sure SelectedIndex is not larger than the number of items in the menu
                        if (_fonts.Count < SelectedIndex)
                            SelectedIndex++;
                        else
                            SelectedIndex = _fonts.Count - 1;
        
                if (_newKeyState.IsKeyDown(Keys.Up) && !_oldKeyState.IsKeyDown(Keys.Up))
                    // Make sure SelectedIndex is not smaller than the number of items in the menu
                        if (_fonts.Count < SelectedIndex)
                            SelectedIndex--;
                        else
                            SelectedIndex = 0;

                if (_newKeyState.IsKeyDown(Keys.Enter) && !_oldKeyState.IsKeyDown(Keys.Enter))
                {
                    //StartGame
                    if (SelectedIndex == 0)
                        _manager.InGame();
                        //Exit //TODO: Fix statiske verdier
                    else if (SelectedIndex == 1)
                        Game.Exit();
                }

                if (_newKeyState.IsKeyDown(Keys.Escape) && !_oldKeyState.IsKeyDown(Keys.Escape))
                    _manager.InGame(); //FIX: BUG  må huske gamle state, ellerno
            }
            //Store the old state
            _oldKeyState = _newKeyState;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AntiqueWhite);

            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            //Change color on font with selectedIndex
            var count = 0;
            foreach (var font in _fonts)
            {
                //Logic for resume game
                if (font == _newGameFont && _manager.GameInProgress == 1)
                    font.FontText = "Resume game";
                    
                font.Color1 = count == SelectedIndex ? Color.Red : Color.Black;      
                _spriteBatch.DrawString(font.Font, font.FontText, font.Position1, font.Color1);
                count++;
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
