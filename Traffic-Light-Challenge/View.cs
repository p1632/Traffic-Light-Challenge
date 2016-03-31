using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Traffic_Light_Challenge
{
    public class View
    {
        private GameEngine gameEngine { get; set; }
        // The "map"
        private BaseField[,] baseField;
        // Volatile is used as hint to the compiler that this data 
        // member will be accessed by multiple threads. 
        private volatile bool _shouldStop;

        public View(GameEngine gameEngine)
        {
            this.gameEngine = gameEngine;
        }

        /// <summary>
        /// Initialize start of the View
        /// </summary>
        private void initializeStart()
        {
            // Prints the map for the first time
            baseField = gameEngine.CurrentMap.BaseField;
            for (int y = 0; y < gameEngine.CurrentMap.Height; y++)
            {
                for (int x = 0; x < gameEngine.CurrentMap.Width; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(" ");
                }
            }


        }

        /// <summary>
        /// The logic of the view
        /// </summary>
        public void Start()
        {
            while (!_shouldStop)
            {
                Console.WriteLine("worker thread: working...");
            }
            Console.WriteLine("View: terminating!");
        }

        /// <summary>
        /// Prints changes
        /// </summary>
        private void printMap()
        {

        }

        /// <summary>
        /// Returns a representative char for each map item.
        /// Throws NotImplementedException, if map item is not implemented.
        /// </summary>
        /// <param name="baseField">A map item</param>
        /// <returns>A representative char for map item</returns>
        private char getChar(BaseField baseField)
        {
            if(baseField is Obstacle)
            {
                return '#';
            }
            else if (baseField is Street)
            {
                return ' ';
            }
            else if (baseField is TrafficLight)
            {
                TrafficLight trafficLight = (TrafficLight)baseField;
                switch (trafficLight.CurrentState)
                {
                    case TrafficLight.State.Green:
                        return '1';
                    case TrafficLight.State.Red:
                        return '0';
                    default:
                        throw new NotImplementedException("View: this TrafficLight.State is not implemented!");
                }
            }
            else
            {
                throw new NotImplementedException("View: this map-type is not implemented!");
            }
        }

        /// <summary>
        /// Sends a siganl for terminating the thread
        /// </summary>
        public void RequestStop()
        {
            _shouldStop = true;
        }
    }
}