using Microsoft.Xna.Framework.Input;

namespace WindowsGame1WithPatterns.Classes.KeyboardConfiguration
{
    //TODO: Not in use per 9 mars 2014, will maybe be removed later
    class KeyboardConfiguration
    {
        public Keys Left { get; set; }
        public Keys Right { get; set; }
        public Keys Up { get; set; }
        public Keys Down { get; set; }

        public Keys Enter { get; set; }
        public Keys Back { get; set; }

        public Keys Jump { get; set; }

       
        public KeyboardConfiguration(Keys left, Keys up, Keys right, Keys down)
        {
            Left = left;
            Up = up;
            Right = right;
            Down = down;
            Back = Keys.Escape;
            Enter = Keys.Enter;
            Jump = Keys.None;
        }

        public KeyboardConfiguration(Keys left, Keys up, Keys right, Keys down, Keys back, Keys enter, Keys jump)
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
