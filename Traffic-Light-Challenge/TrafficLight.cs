using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Traffic_Light_Challenge
{
    public class TrafficLight : BaseField
    {
        public enum State{ Red, Green };
        public TrafficLight()
        {
            throw new System.NotImplementedException();
        }

        public State CurrentState { get; set; }
    }
}