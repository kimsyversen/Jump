using Microsoft.Xna.Framework.Input;

namespace WindowsGame1WithPatterns.Classes.KeyboardConfiguration
{
    /// <summary>
    /// Used to create key mappings for players
    /// </summary>
    class KeyboardMapping
    {
        public Keys Left { get; set; }
        public Keys Right { get; set; }
        public Keys Up { get; set; }
        public Keys Down { get; set; }

        public Keys Enter { get; set; }
        public Keys Back { get; set; }

        public Keys Jump { get; set; }

        public KeyboardMapping(Keys left, Keys right, Keys jump)
        {
            Left = left;
            Right = right;
            Back = Keys.Escape;
            Enter = Keys.Enter;
            Jump = jump;
        }

        public KeyboardMapping(Keys left, Keys up, Keys right, Keys down)
        {
            Left = left;
            Up = up;
            Right = right;
            Down = down;
            Back = Keys.Escape;
            Enter = Keys.Enter;
            Jump = Keys.None;
        }

        public KeyboardMapping(Keys left, Keys up, Keys right, Keys down, Keys back, Keys enter, Keys jump)
        {
            Left = left;
            Up = up;
            Right = right;
            Down = down;
            Back = back;
            Down = down;
            Enter = enter;
            Jump = jump;
        }
    }
}
