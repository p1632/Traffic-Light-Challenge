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
        private List<TrafficLight> TrafficLightList;
        /// <summary>
        /// initializes the GameEngine with 
        /// default number of cars(2)
        /// default map(map index = 1)
        /// </summary>
        public GameEngine()
        {
            init();
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
            init();
            numberOfCars = NumberOfCars;
            mapIndex = MapIndex;
        }

        private void init()
        {
            TrafficLightList = new List<TrafficLight>();
        }

        private DAOMap DAOMap
        {
            get;
            set;
        }

        public Map CurrentMap
        {
            get;
            private set;
        }

        private CarEngine CarEngine
        {
            get;
            set;
        }

        public void Start()
        {
            //get map
            CurrentMap = DAOMap.LoadMap(mapIndex);

            //scan map for traffic lights
            scanMapForTrafficLights();

            //initialize CarEngine
            CarEngine = new CarEngine();

            //initialize gameloop
            Timer gameTicker = new Timer(500);
            gameTicker.Elapsed += gameLoop;

            //start gameloop
            gameTicker.Start();
        }

        private void gameLoop(object sender, ElapsedEventArgs e)
        {
            //move cars
            //change traffic lights
        }

        public void RequestStop()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Scans CurrentMap for TrafficLights and adds them to TrafficLightList
        /// </summary>
        private void scanMapForTrafficLights()
        {
            for (int row = 0; row < CurrentMap.Height; row++)
            {
                for (int column = 0; column < CurrentMap.Width; column++)
                {
                    if(CurrentMap.BaseField[row,column] is TrafficLight)
                    {
                        TrafficLightList.Add((TrafficLight)CurrentMap.BaseField[row, column]);
                    }
                }
            }
        }

    }
}