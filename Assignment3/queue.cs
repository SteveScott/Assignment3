using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment3
{
    public class  Queue
    {   public List<Passenger> passengers = new List<Passenger>();
        public int MaxLength { get; set; }
        private int privateProcessingRate;

        public Queue(int processingRate)
        {
            this.MaxLength = 0;
            privateProcessingRate = processingRate;
        }

        public void AddPassenger(Passenger passenger)
        {
            passengers.Add(passenger);
            int currentLength = passengers.Count;
            if (currentLength > MaxLength)
            {
                MaxLength = currentLength;
            }

        }

        public bool PlacePassenger()
        {
            if (passengers.Count != 0)
            {
                Passenger thisPassenger = passengers.First();
                bool isFound;

                if (thisPassenger.IsFirstClass == true)
                {
                    isFound = FindStation(thisPassenger, Globals.firstClassTicketCounters);
                    if (isFound == true)
                    {
                        passengers.RemoveAt(0);
                        return isFound;
                    }
                    else
                    {
                        return isFound;

                    }
                }
                else
                {
                    isFound = FindStation(thisPassenger, Globals.economyClassTicketCounters);
                    if (isFound == true)
                    {
                        passengers.RemoveAt(0);
                        return isFound;
                    }
                    else
                    {
                        isFound = FindStation(thisPassenger, Globals.firstClassTicketCounters);
                        if (isFound == true)
                        {
                            passengers.RemoveAt(0);
                            return isFound;
                        }
                        else
                        {
                            return isFound;
                        }
                    }

                }

            }
            else
            {
                return false;
            }
            
            
            
       

        }

        public bool FindStation(Passenger passenger, TicketCounter[] counters)
        {
            
            counters.ShuffleArray();

            //if the counter isn't occupied, add the passenger, else continue to the next passenger.  If there are no more unoccupied ticket counters, return false.
            for (int i = 0; i < counters.Length; i++)
            {
                TicketCounter thisCounter = counters[i];
                if (thisCounter.IsOccupied != true)
                {
                    thisCounter.AddPassenger(passenger);
                    return true;
                }
                else
                {
                    continue;
                }
            }

            return false;
        }




    }
}
