using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Traffic_Light_Challenge
{
    /// <summary>
    /// Class where the definition of the map is stored.
    /// Does not change once set by DAOMap Class.
    /// </summary>
    public class Map
    {
        /// <summary>
        /// Constructor for the Map with all Parameters.
        /// Is set in DAOMap
        /// </summary>
        /// <param name="ID">The Map ID</param>
        /// <param name="Width">Width of the map array</param>
        /// <param name="Height">Height of the map array</param>
        /// <param name="BaseField"></param>
        public Map(uint ID, uint Width, uint Height, BaseField[,] BaseField, bool ATGL)
        {
            this.ID = ID;
            this.Width = Width;
            this.Height = Height;
            this.BaseField = BaseField;
            this.AGTL = ATGL;
        }

        /// <summary>
        /// Identification for the Map, eventually used to choose from maps
        /// </summary>
        public uint ID { get; set; }
        public uint Width { get; set; }
        public uint Height { get; set; }
        /// <summary>
        /// The Map
        /// </summary>
        public BaseField[,] BaseField { get; set; }
        /// <summary>
        /// Places where cars can start
        /// </summary>
        public Point[] StartingPoint { get; set; }
        /// <summary>
        /// Auto generate Traffic Light
        /// </summary>
        public bool AGTL;
    }
}