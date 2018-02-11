using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3
{
    public class TicketCounter
    {
        public int uptime;
        int countdownTimer;
        int firstClassProcessingTime;
        int economyClassProcessingTime;
        public bool IsOccupied { get; set; }

        public Passenger[] passenger = new Passenger[1];

        public TicketCounter(int fcProcessingTime, int ecProcessingTime)
        {
            uptime = 0;
            countdownTimer = 0;
            firstClassProcessingTime = fcProcessingTime;
            economyClassProcessingTime = ecProcessingTime;
            IsOccupied = false;
            //Console.WriteLine("counter created!");
        }

        public void ProcessPassenger()
        {
            if (passenger[0] != null)
            {
                IsOccupied = true;
                AdvanceUptime();
                //Console.WriteLine("countdownTimer:" + countdownTimer);
                countdownTimer -= 1;
                if (countdownTimer == 0)
                {
                    Passenger thisPassenger = passenger[0];
                    Globals.processedPassengers.Add(thisPassenger);
                    passenger[0] = null;
                    IsOccupied = false;
                }
            }
        }

        public void AddPassenger(Passenger incomingPassenger)
        {
            passenger[0] = incomingPassenger;
            if (incomingPassenger.IsFirstClass == true)
            {
                countdownTimer = firstClassProcessingTime;
            }
            else
            {
                countdownTimer = economyClassProcessingTime;
            }
        }

        public void AdvanceUptime()
        {
            uptime += 1;
        }
    }
}
