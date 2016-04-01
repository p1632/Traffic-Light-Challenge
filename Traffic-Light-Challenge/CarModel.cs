using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Traffic_Light_Challenge
{
    public class CarModel
    {
        public enum Direction { N,E,S,W };
        public List<Direction> Path;
        public int X { get; set; }
        public int Y { get; set; }
        public Direction CurrentDirection { get; set; }
    }
}