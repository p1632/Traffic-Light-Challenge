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

        public CarModel[] CarModel { get; private set; }

        public CarEngine()
        {
            createPath();
        }


        /// <summary>
        /// STUB METHOD so far
        /// </summary>
        private void createPath()
        {

        }
    }
}