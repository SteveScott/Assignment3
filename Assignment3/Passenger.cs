using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3
{
    public class Passenger
    {
        int waitTime = 0;
        public bool IsFirstClass { get; set; }

        public Passenger( bool isFirstClass)
        {
            IsFirstClass = isFirstClass;
        }
        
        public int GetWaitTime()
        {
            return waitTime;
        }

        public void AdvanceTimer()
        {
            waitTime += 1;
        }

    }
}
