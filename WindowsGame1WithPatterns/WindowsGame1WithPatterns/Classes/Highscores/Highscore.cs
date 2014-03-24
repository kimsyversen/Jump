using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Jump.Classes.FileIO;

namespace Jump.Classes.Highscores
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
        private const string FileName = @"highscore";
        private readonly string _filepath = AppDomain.CurrentDomain.BaseDirectory;

        private List<Score> _highscores;
        private const int HighscoreLimit = 3;

        public List<Score> Highscores
        {
            get { return _highscores.AsReadOnly().ToList(); }
        }

        public bool IsNewHighscore(int score)
        {
            if (_highscores.Count < HighscoreLimit)
                return true;

            foreach (var highscore in _highscores)
                if (score > highscore.Points)
                    return true;
            return false;
        }

        public bool AddScore(Score score)
        {
            //Search where to put the score in the list
            var index = -1;
            foreach (var highscore in _highscores)
                if (highscore.Points < score.Points)
                {
                    index = _highscores.IndexOf(highscore);
                    break;
                }

            //The score was to low to go in the highscore list
            if (index <= -1)
                //But it can go in to the highscore if it is not filled up.
                if (_highscores.Count < HighscoreLimit)
                    _highscores.Add(score);
                else
                    return false;
            else
                _highscores.Insert(index, score);
            
            

            //If the highscore is larger than it is allowed to be, remove
            //the last element of the list.
            if (_highscores.Count > HighscoreLimit)
                _highscores.RemoveAt(_highscores.Count - 1);

            //Save the highscore list
            Save();

            return true;
        }

        private void Save()
        {
            Serializer.Serialize<List<Score>>(_highscores, _filepath + FileName);
        }

        private void Load()
        {
            try
            {
                _highscores = Serializer.Deserialize<List<Score>>(_filepath + FileName);
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
