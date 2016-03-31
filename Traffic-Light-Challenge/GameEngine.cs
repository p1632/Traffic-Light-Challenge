using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Traffic_Light_Challenge
{
    public class GameEngine
    {
        private uint numberOfCars;
        private uint mapIndex;
        /// <summary>
        /// initializes the Game Engine with 
        /// default number of cars(2) and 
        /// default map(Map index = 1)
        /// </summary>
        public GameEngine()
        {
            //Setting default settings
            numberOfCars = 2;
            mapIndex = 1;
        }

        /// <summary>
        /// initializes the Game Engine with 
        /// NumberOfCars and MapIndex
        /// </summary>
        public GameEngine(uint NumberOfCars, uint MapIndex)
        {
            numberOfCars = NumberOfCars;
            mapIndex = MapIndex;
        }

        private DAOMap DAOMap
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Map CurrentMap
        {
            get;
            private set;
        }

        private CarEngine[] CarEngine
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public void Start()
        {
            //get map
            CurrentMap = DAOMap.LoadMap(mapIndex);
            //initialize CarEngines
            CarEngine = new CarEngine[numberOfCars];

            //initialize gameloop
            Timer gameTicker = new Timer(500);
            gameTicker.Elapsed += gameLoop;

            //start gameloop
            gameTicker.Start();
        }

        private void gameLoop(object sender, ElapsedEventArgs e)
        {
            for (int i = 0; i < numberOfCars; i++)
            {
                //move cars
                //not decided if movement happens here or in carEngine
            }
        }

        public void RequestStop()
        {
            throw new System.NotImplementedException();
        }
    }
}