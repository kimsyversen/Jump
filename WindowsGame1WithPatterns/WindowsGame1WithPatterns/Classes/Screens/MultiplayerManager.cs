﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
//using WindowsGame1WithPatterns.Classes.CameraConfiguration;
using System.Xml.Linq;
using WindowsGame1WithPatterns.Classes.KeyboardConfiguration;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Platform;
using WindowsGame1WithPatterns.Classes.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using WindowsGame1WithPatterns.Classes;
using WindowsGame1WithPatterns.Classes.Sprites;
using WindowsGame1WithPatterns.Classes.Sprites.Factories;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Platform;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Fonts;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player;
using WindowsGame1WithPatterns.Classes.CameraConfiguration;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;


namespace WindowsGame1WithPatterns.Classes.Screens
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    internal class MultiplayerManager : AbstractPlayerManager
    {
        private const int NumberOfPlayers = 2;

        public MultiplayerManager(Game game, SpriteBatch spriteBatch, string managerId, GraphicsDeviceManager graphics)
            : base(game, spriteBatch, managerId, GameStates.MultiplayerManager, graphics, NumberOfPlayers)
        {
        }
    }
}

