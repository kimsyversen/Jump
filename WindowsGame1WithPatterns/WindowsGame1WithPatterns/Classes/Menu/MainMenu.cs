using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1WithPatterns.Classes.Menu
{
    class MainMenu
    {
        private bool initialized = false;
        private List<MenuItem> menuItems { get; set; }
        public string Title { get; set; }
        public string InfoText { get; set; }
        private int lastNavigated { get; set; }


        public MainMenu(string title)
        {
            menuItems = new List<MenuItem>();
            Title = title;
            InfoText = "";
        }

        public MainMenu(string title, string infoText)
        {
            menuItems = new List<MenuItem>();
            Title = title;
            InfoText = infoText;
        }


        public int Count
        {
            get { return menuItems.Count; }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            protected set
            {
                if (value >= menuItems.Count || value < 0)
                    throw new ArgumentOutOfRangeException();
                
                _selectedIndex = value;
            }
        }
        public MenuItem SelectedItem
        {
            get
            {
                return menuItems[SelectedIndex];
            }
        }

        public virtual void AddMenuItem(string title, Action<Buttons> action)
        {
            AddMenuItem(title, action, "");
        }

        public virtual void AddMenuItem(string title, Action<Buttons> action, string description)
        {
            menuItems.Add(new MenuItem { Title = title, Description = description, Action = action });
            SelectedIndex = 0;
        }
    }
}
