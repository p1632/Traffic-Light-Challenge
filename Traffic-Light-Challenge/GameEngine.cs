using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Traffic_Light_Challenge
{
    public class GameEngine
    {
        #region Variables and Properties
        private uint numberOfCars;
        private uint mapIndex;
        private List<TrafficLight> trafficLight;
        private List<Street> startPosition;
        private DAOMap DAOMap { get; set; } 
        public Map CurrentMap { get; private set; } 
        private CarEngine CarEngine { get; set; }
        #endregion
        #region Constructors
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
        #endregion
        #region Start and Stop
        public void Start()
        {
            //get map
            
            CurrentMap = DAOMap.LoadMap(mapIndex);

            //scan map for traffic lights and starting positions
            scanMapForTrafficLights();
            scanMapForStartPoints();

            //initialize CarEngine
            CarEngine = new CarEngine();

            //initialize gameloop
            Timer gameTicker = new Timer(500);
            gameTicker.Elapsed += gameLoop;

            //start gameloop
            gameTicker.Start();
        }

        public void RequestStop()
        {
            throw new System.NotImplementedException();
        }
        #endregion
        #region Game
        private void gameLoop(object sender, ElapsedEventArgs e)
        {
            //move cars
            //change traffic lights

        }
        #endregion
        #region Methods for initializing and start
        private void init()
        {
            trafficLight = new List<TrafficLight>();
            DAOMap = JsonDAOMap.getInstance();
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
                        trafficLight.Add((TrafficLight)CurrentMap.BaseField[row, column]);
                    }
                }
            }
        }

        /// <summary>
        /// Scans the border of the map for streets and adds them to startPosition List
        /// </summary>
        private void scanMapForStartPoints()
        {
            for (int row = 0; row < CurrentMap.Height; row++)
            {
                if (CurrentMap.BaseField[row, 0] is Street)
                    startPosition.Add((Street)CurrentMap.BaseField[row, 0]);
            }
            for (int row = 0; row < CurrentMap.Height; row++)
            {
                if (CurrentMap.BaseField[row, CurrentMap.Width - 1] is Street)
                    startPosition.Add((Street)CurrentMap.BaseField[row, CurrentMap.Width - 1]);
            }
            for (int column = 0; column < CurrentMap.Height; column++)
            {
                if (CurrentMap.BaseField[0, column] is Street)
                    startPosition.Add((Street)CurrentMap.BaseField[0, column]);
            }
            for (int column = 0; column < CurrentMap.Height; column++)
            {
                if (CurrentMap.BaseField[CurrentMap.Height - 1, column] is Street)
                    startPosition.Add((Street)CurrentMap.BaseField[CurrentMap.Height - 1, column]);
            }
        }
        #endregion
    }
}