using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Traffic_Light_Challenge
{
    public class CarEngine
    {
        public enum Direction { N, E, S, W };
        private int indexPath = 0;
        private List<Direction> path;

        public CarModel CarModel { get; private set; }

        public CarEngine()
        {
            createPath();
        }

        public void MoveCar(int x, int y)
        {
            CarModel.X = x;
            CarModel.Y = y;
        }

        /// <summary>
        /// Gets the Next direction
        /// </summary>
        public Direction NextDirection
        {
            get {
                //To prevent IndexOutOfRangeExeption
                if(indexPath < path.Count)      
                    return path[indexPath++];

                //Unreachable code if path is valid
                return Direction.W;                             
            }
        }

        /// <summary>
        /// STUB METHOD so far
        /// </summary>
        private void createPath()
        {
            path = new List<Direction> { Direction.E };
        }
    }
}