using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts.Concretes
{
    class CoordinateFontSprite : Sprite, IFont
    {
        //Reference to the game. Needed because we need to get some properties like size of screen
        private Game _game;
        private SpriteFont _font;

        //Reference to the player associated with this font
        private readonly IPlayer _subject;
        
        private String _coordinateString ;
        private Vector2 _coordinates;

        public CoordinateFontSprite(Game game, IPlayer playerSubject)
        {
            _game = game;
            _subject = playerSubject;
            _subject.RegisterFontObserver(this);

            
            _font = game.Content.Load<SpriteFont>("Coordinates");
            _coordinateString = "moved yet";
            Color = Color.LightGreen;
            Rotate = 0;
            Origin = _font.MeasureString(_coordinateString) / 2;
            Scale = 1.0f;
            SpriteEffects = SpriteEffects.None;
            Position = new Vector2(_game.Window.ClientBounds.Width - _font.MeasureString(_coordinateString).X,
                                         0 + _font.MeasureString(_coordinateString).Y);
        }

        public void Update(GameTime gameTime, Rectangle clientBounds)
        {
            _coordinateString = _coordinates.ToString();
        }

        public void UpdateCoordinates(Vector2 coordinates)
        {
            _coordinates = coordinates;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, _coordinateString, Position, Color, Rotate, Origin , Scale, SpriteEffects, 0.5f);
        }

    }
}
