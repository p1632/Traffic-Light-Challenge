using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Traffic_Light_Challenge
{
    public class Map
    {
        public Map(uint ID, uint Width, uint Height, BaseField[,] BaseField)
        {
            this.ID = ID;
            this.Width = Width;
            this.Height = Height;
            this.BaseField = BaseField;
        }

        public uint ID { get; set; }
        public uint Width { get; set; }
        public uint Height { get; set; }
        public BaseField[,] BaseField { get; set; }
    }
}