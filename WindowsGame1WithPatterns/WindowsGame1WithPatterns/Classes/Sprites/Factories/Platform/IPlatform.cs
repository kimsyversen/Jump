using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Platform
{
    internal interface IPlatform : ISprite
    {
        Rectangle DetectCollition { get; }
        Vector2 FloorPosition { get; set; }
        Texture2D FloorTexture { get; set; }
    }
}
