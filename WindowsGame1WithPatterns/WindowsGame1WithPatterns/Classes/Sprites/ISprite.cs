using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace WindowsGame1WithPatterns.Classes.Sprites
{
    //General "things" all sprites can do
    interface ISprite
    {
        Vector2 Position { get; set; }
        string Name { get; set; }
    }
}
