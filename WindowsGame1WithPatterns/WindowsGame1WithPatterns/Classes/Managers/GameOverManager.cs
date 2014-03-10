using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WindowsGame1WithPatterns.Classes.KeyboardConfiguration;
using WindowsGame1WithPatterns.Classes.Sprites.Factories;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts.Concretes.MenuFonts;

namespace WindowsGame1WithPatterns.Classes.Managers.Magnus
{
    class GameOverManager : StateManager
    {
        private SimpleFont _gameOverFont;
        private SpriteBatch _spriteBatch;

        public GameOverManager(Game game, string managerId)
            : base(game, managerId, States.GameOver)
        {
            _gameOverFont = new SimpleFont(game, game.Content.Load<SpriteFont>("Menu/GameOver/GameOver"),
                                        "Game over! Press K to restart", Color.Black,
                                        new Vector2(game.Window.ClientBounds.Width / 2f,
                                                    game.Window.ClientBounds.Height / 2f));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            if (KeyboardManager.KeyJustPressed(Keys.K))
                ChangeStateTo(States.MainMenu);

            base.Update(gameTime);

        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AntiqueWhite);


            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            _spriteBatch.DrawString(_gameOverFont.Font, _gameOverFont.FontText, _gameOverFont.Position1, _gameOverFont.Color1);

            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
