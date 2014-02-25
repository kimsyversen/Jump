using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.Sprites
{
    //General "things" all sprites can do
    interface ISprite
    {
        //All sprites may be updated and drawn
        void Update(GameTime gameTime, Rectangle clientBounds);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
