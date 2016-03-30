using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Traffic_Light_Challenge
{
    public class View
    {
        private GameEngine gameEngine { get; set; }

        public View(GameEngine gameEngine)
        {
            this.gameEngine = gameEngine;
        }

        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void RequestStop()
        {
            throw new System.NotImplementedException();
        }
    }
}