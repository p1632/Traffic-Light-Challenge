using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Traffic_Light_Challenge
{
    public class CarModel
    {
        public enum Orientation { N,O,S,W };

        public int X { get; set; }
        public int Y { get; set; }
        public Orientation CurrentOrientation { get; set; }
    }
}