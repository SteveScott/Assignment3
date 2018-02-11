using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3
{
    public class Clock
    {
        public int Time { get; set; }
  
        int firstClassArrivalRate;
        int economyClassArrivalRate;
        int firstClassServiceRate;
        int economyClassServiceRate;

        public Clock(int newDuration, int arrivalFirstClass, int arrivalEconomy, int processingFirstClass, int processingEconomy)
        {
            Time = 0;
    
            firstClassArrivalRate = arrivalFirstClass;
            economyClassArrivalRate = arrivalEconomy;
            firstClassServiceRate = processingFirstClass;
            economyClassServiceRate = processingEconomy;
            Console.Out.WriteLine("Clock successfully created.");
        }

        public void Advance()
        {
            Time += 1;
        }

        

    }
}
