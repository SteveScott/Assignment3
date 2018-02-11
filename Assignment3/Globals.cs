using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3
{
    public static class Globals
    {
        public static TicketCounter[] firstClassTicketCounters;
        public static  TicketCounter[] economyClassTicketCounters;

        public static  List<Passenger> processedPassengers = new List<Passenger>();

        public static void SetNumberFirstClassCounters(int countFirstClassTicketCounters)
        {
            firstClassTicketCounters = new TicketCounter[countFirstClassTicketCounters];
        }

        public static void SetNumberEconomyClassCounters(int countEconomyClassTicketCounters)
        {
            economyClassTicketCounters = new TicketCounter[countEconomyClassTicketCounters];
        } 
    }
}
