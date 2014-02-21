using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace WindowsGame1WithPatterns.Classes.Sprites
{
    interface ISprite
    {
        Vector2 Position { get; set; }
        string Name { get; set; }
    }
}
