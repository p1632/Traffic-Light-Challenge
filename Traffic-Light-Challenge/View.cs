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
        // The "cars"
        private List<CarModel> cars;
        // Volatile is used as hint to the compiler that this data 
        // member will be accessed by multiple threads. 
        private volatile bool _shouldStop;
        private bool mapChanged;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gameEngine">A reference to the game engine</param>
        public View(GameEngine gameEngine)
        {
            this.gameEngine = gameEngine;
        }

        /// <summary>
        /// Initialize start of the View
        /// </summary>
        private void initializeStart()
        {
            // Prints map for the first time
            baseField = gameEngine.CurrentMap.BaseField;
            for (int y = 0; y < gameEngine.CurrentMap.Height; y++)
            {
                for (int x = 0; x < gameEngine.CurrentMap.Width; x++)
                {
                    printChar(x, y, getMapChar(baseField[x, y]));
                }
            }

            // Prints cars for the first time     
            cars = gameEngine.GetCars;
            foreach(CarModel car in cars)
            {
                printChar(car.X, car.Y, getCarChar(car));
            }
        }

        /// <summary>
        /// Prints a char on the right position
        /// </summary>
        /// <param name="x">Column coordinate</param>
        /// <param name="y">Row coordinate</param>
        /// <param name="c">Char to be printed</param>
        private void printChar(int x, int y, char c)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(c);
        }

        /// <summary>
        /// The logic of the view
        /// </summary>
        public void Start()
        {
            initializeStart();
            while (!_shouldStop)
            {
                print();
            }
            Console.WriteLine("View: terminating!");
        }

        /// <summary>
        /// Prints everything
        /// </summary>
        private void print()
        {
            printMap();
            printCars();
        }

        /// <summary>
        /// Prints list of cars on the map
        /// </summary>
        private void printCars()
        {
            // For performance reasons, only print new car positions

            List<CarModel> newCars = gameEngine.GetCars;
            // Cars to "delete"
            foreach (CarModel car in getCarsWhichAreNotInOriginalList(newCars, cars))
            {
                printChar(car.X, car.Y, getMapChar(baseField[car.X, car.Y]));
            }
            // Cars to print
            foreach (CarModel car in getCarsWhichAreNotInOriginalList(cars, newCars))
            {
                printChar(car.X, car.Y, getCarChar(car));
            }
            cars = newCars;
        }

        private List<CarModel> getCarsWhichAreNotInOriginalList(List<CarModel> originalList, List<CarModel> newList)
        {
            List<CarModel> carsWhichAreNotInOriginalList = new List<CarModel>();
            bool carExists;
            foreach(CarModel car in newList)
            {
                carExists = false;
                foreach(CarModel origCar in originalList)
                {
                    if ((car.X == origCar.X) && (car.Y == origCar.Y))
                    {
                        carExists = true;
                        break;
                    }
                }
                if (!carExists)
                {
                    carsWhichAreNotInOriginalList.Add(car);
                }
            }

            return carsWhichAreNotInOriginalList;
        }

        /// <summary>
        /// Prints changes to the map,
        /// like the change of the state of a traffic light
        /// </summary>
        private void printMap()
        {
            BaseField [,] newBaseField = gameEngine.CurrentMap.BaseField;
            mapChanged = false;
            for (int y = 0; y < gameEngine.CurrentMap.Height; y++)
            {
                for (int x = 0; x < gameEngine.CurrentMap.Width; x++)
                {
                    if (baseField[x, y] != newBaseField[x, y])
                    {
                        if (!mapChanged)
                        {
                            mapChanged = true;
                        }
                        printChar(x, y, getMapChar(newBaseField[x, y]));
                    }
                }
            }
            if (mapChanged)
            {
                baseField = newBaseField;
            }
        }

        /// <summary>
        /// Returns a representative char for a car
        /// </summary>
        /// <param name="car">the car to be printed</param>
        /// <returns>the representative char for the car</returns>
        private char getCarChar(CarModel car)
        {
            // Maybe print each car(ID) different ???
            return 'o';
        }

        /// <summary>
        /// Returns a representative char for each map item.
        /// Throws NotImplementedException, if map item is not implemented.
        /// </summary>
        /// <param name="baseField">A map item</param>
        /// <returns>A representative char for map item</returns>
        private char getMapChar(BaseField baseField)
        {
            // '?' should never be displayed
            char mapChar = '?';

            if(baseField is Obstacle)
            {
                mapChar = '#';
            }
            else if (baseField is Street)
            {
                mapChar = ' ';
            }
            else if (baseField is TrafficLight)
            {
                TrafficLight trafficLight = (TrafficLight)baseField;
                switch (trafficLight.CurrentState)
                {
                    case TrafficLight.State.Green:
                        mapChar = '1';
                        break;
                    case TrafficLight.State.Red:
                        mapChar = '0';
                        break;
                    default:
                        throw new NotImplementedException("View: this TrafficLight.State is not implemented!");
                }
            }
            else
            {
                throw new NotImplementedException("View: this map-type is not implemented!");
            }

            return mapChar;
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