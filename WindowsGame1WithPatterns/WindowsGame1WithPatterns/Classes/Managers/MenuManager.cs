using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1WithPatterns.Classes.Managers
{
    class MenuManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public MenuManager(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize(); 
        }


        public override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
                Game.Exit();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }


    }
}
