using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Platform;

namespace WindowsGame1WithPatterns.Classes.Sprites.Factories.Player
{
    //"Things" that only players can do
    interface IPlayer : ISprite
    {
        Vector2 PlayerSpeed { get; set; }
        Vector2 PlayerPosition { get; set; }
        bool HasJumped { get; set; }
        bool HasHitTheWall { get; set; }
        bool HasHitPlatform { get; set; }
        IPlatform OnPlatform { get; set; }
        Rectangle DetectCollision { get; }
        float GetY { get; set; }
        Texture2D PlayerTexture { get; }

       

        //A player may have observers (fonts that shall be updated, ex. score)
        void RegisterFontObserver(IFont observer);
        void RemoveFontObserver(IFont observer);
        void NotifyFontObservers();

    }
}
