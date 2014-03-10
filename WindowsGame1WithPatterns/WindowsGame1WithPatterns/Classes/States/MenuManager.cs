using System.Collections.Generic;
using WindowsGame1WithPatterns.Classes.KeyboardConfiguration;
using WindowsGame1WithPatterns.Classes.Sprites.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts.Concretes;

namespace WindowsGame1WithPatterns.Classes.States
{
    class MenuManager : State
    {
        private SpriteFactory _spriteFactory;
        SpriteBatch _spriteBatch;
        private List<SimpleFont> _fonts;
        private SimpleFont _newGameFont;
        private SimpleFont _exitFont;
        public int SelectedIndex = 0;

        private Microsoft.Xna.Framework.Game _game;

        public MenuManager(Microsoft.Xna.Framework.Game game, string managerId) : base(game, managerId, GameStates.MainMenu)
        {
            _game = game;
        }

        public override void Initialize()
        {
            _fonts = new List<SimpleFont>();
            _spriteFactory = new SpriteFactory(Game);

            var fontFactory = _spriteFactory.CreateFontFactory();

            _newGameFont = fontFactory.Font("New Game", Color.Black,
                                            new Vector2(_game.Window.ClientBounds.Width / 2f,
                                                        _game.Window.ClientBounds.Height / 2f));
            _exitFont = fontFactory.Font("Exit", Color.Black,
                                            new Vector2(_game.Window.ClientBounds.Width / 2f,
                                                        _game.Window.ClientBounds.Height / 3f));

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
            //Stay inside the indexes that exist in the list
            if (SelectedIndex < _fonts.Count && SelectedIndex >= 0)
            {
                if (KeyboardManager.IsKeyDown(Keys.Down))
                    // Make sure SelectedIndex is not larger than the number of items in the menu
                    if (_fonts.Count < SelectedIndex)
                        SelectedIndex++;
                    else
                        SelectedIndex = _fonts.Count - 1;

                if (KeyboardManager.IsKeyDown(Keys.Up))
                    // Make sure SelectedIndex is not smaller than the number of items in the menu
                    if (_fonts.Count < SelectedIndex)
                        SelectedIndex--;
                    else
                        SelectedIndex = 0;

                if (KeyboardManager.KeyJustPressed(Keys.Enter))
                {
                    //StartGame, switch state to InGame
                    if (SelectedIndex == 0)
                        ChangeStateTo(GameStates.InGame);
                    else if (SelectedIndex == 1) //TODO: Fix statiske verdier
                        Game.Exit();
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AntiqueWhite);

            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            //TODO: Change color on font with selectedIndex
            foreach (var font in _fonts)
                
                _spriteBatch.DrawString(font.Font, font.FontText, font.Position1, font.Color1);

            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
