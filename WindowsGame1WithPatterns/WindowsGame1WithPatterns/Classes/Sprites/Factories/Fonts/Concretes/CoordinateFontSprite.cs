﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts.Concretes
{
    class FontSprite : IFont
    {
        //Reference to the game. Needed because we need to get some properties like size of screen
        private Game _game;
        private SpriteFont _font;
        //Reference to the player associated with this font
        private readonly IPlayer _subject;
        private String _coordinateString;
        private Vector2 _coordinates;

        public FontSprite(Game game, IPlayer playerSubject)
        {
            _game = game;
            _subject = playerSubject;
            _subject.RegisterObserver(this);
            _font = game.Content.Load<SpriteFont>("Coordinates");
        }

        public void Update(GameTime gameTime, Rectangle clientBounds)
        {
            _coordinateString = _coordinates.ToString();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.DrawString(_font, _coordinateString, new Vector2(_game.Window.ClientBounds.Width - _font.MeasureString(_coordinateString).X, 0 + _font.MeasureString(_coordinateString).Y), Color.LightGreen,
       0, _font.MeasureString(_coordinateString) / 2, 1.0f, SpriteEffects.None, 0.5f);

            
           // base.Draw(gameTime); //Sprite bør endres litt sånn at den er tilpasset fontsprite OG players. Font trenger bare update, draw og pos, farge, effect + noen fler?
        }

        public void UpdateCoordinates(Vector2 coordinates)
        {
            _coordinates = coordinates;
        }
    }
}
