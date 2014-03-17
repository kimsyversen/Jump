using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace WindowsGame1WithPatterns.Classes.Components
{
    class AudioComponent : GameComponent
    {
        //TODO: Implement or remove?
        private SoundEffect _soundEngine;
        public AudioComponent(Game game) : base(game)
        {
        }
    }
}
