﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame1WithPatterns.Classes.Highscores;
using WindowsGame1WithPatterns.Classes.FileIO;
using System.Diagnostics;

namespace WindowsGame1WithPatterns.Classes.Highscore
{
    class Highscore
    {
        /// <summary>
        /// Sorce: http://csharpindepth.com/Articles/General/Singleton.aspx
        /// Singleton implementation 
        /// </summary>
        #region Singleton
        private static readonly Lazy<Highscore> lazy =
        new Lazy<Highscore>(() => new Highscore());
    
        public static Highscore Instance { get { return lazy.Value; } }

        private Highscore()
        {
            Load();
        }
        #endregion

        private string _fileName = @"highscore";
        private string _filepath = AppDomain.CurrentDomain.BaseDirectory;

        private List<Score> _highscores;
        private int highscoreLimit = 5;

        public bool IsNewHighscore(int score)
        {
            foreach (var highscore in _highscores)
                if (score > highscore.Points)
                    return true;
            return false;
        }

        public bool AddScore(Score score)
        {
            var index = -1;
            foreach (var highscore in _highscores)
                if (highscore.Points < score.Points)
                {
                    index = _highscores.IndexOf(highscore);
                    break;
                }

            //The score was to low to go in the highscore list
            if (index <= -1) return false;

            _highscores.Insert(index, score);

            //If the highscore is larger than it is allowed to be, remove
            //the last element of the list.
            if (_highscores.Count > highscoreLimit)
                _highscores.RemoveAt(_highscores.Count - 1);

            //Save the highscore list
            Save();

            return true;
        }

        private void Save()
        {
            Serializer.Serialize<List<Score>>(_highscores, _filepath + _fileName);
        }

        private void Load()
        {
            try
            {
                _highscores = Serializer.Deserialize<List<Score>>(_filepath + _fileName);
                _highscores = _highscores.OrderByDescending(o => o.Points).ToList();
            }
            catch (Exception)
            {
                Debug.WriteLine("Could not load the highscore file.");
                _highscores = new List<Score>();
            }
        }
    }
}
