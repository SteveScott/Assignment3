using System;
using System.Collections.Generic;

namespace Assignment3
{
    class Program
    {
        static void Main(string[] args)
        {
         //declare variables
            int duration = 0;
            int FirstClassArrivalRate = 0;
            int EconomyClassArrivalRate = 0;
            int FirstClassServiceRate = 0;
            int EconomyClassServiceRate = 0;
            int FirstClassCounters = 0;
            int EconomyClassCounters = 0;
          

            List<Passenger> bufferFirstClass = new List<Passenger>();
            List<Passenger> bufferEconomyClass = new List<Passenger>();
            List<Passenger> bufferAll = new List<Passenger>();

         
            Console.Out.WriteLine("################################");
            Console.Out.WriteLine("###                          ###");
            Console.Out.WriteLine("###       Ticket Counter     ###");
            Console.Out.WriteLine("###     Queuing Simulation   ###");
            Console.Out.WriteLine("###                          ###");
            Console.Out.WriteLine("################################");
            Console.Out.WriteLine("");
            Console.Out.WriteLine("");

    

         //get arrival rate from console
            while (FirstClassArrivalRate == 0)
            {
                Console.Out.WriteLine("Enter first class passenger arrival rate:");
                FirstClassArrivalRate = Input.InputText();
            }

            while (EconomyClassArrivalRate == 0)
            {
                Console.Out.WriteLine("Enter economy class passenger arrival rate:");
                EconomyClassArrivalRate = Input.InputText();
            }

        //get number of ticket counters
            while (FirstClassCounters == 0)
            {
                Console.Out.WriteLine("Enter the number of first class ticket counters:");
                FirstClassCounters = Input.InputText();
            }

            while (EconomyClassCounters == 0)
            {
                Console.Out.WriteLine("Enter the number of economy class ticket counters:");
                EconomyClassCounters = Input.InputText();
            }

         //get service rate from console
            while (FirstClassServiceRate == 0)
            {
                Console.Out.WriteLine("Enter processing time of first class ticket counters:");
                FirstClassServiceRate = Input.InputText();
            }

            while (EconomyClassServiceRate == 0)
            {
                Console.Out.WriteLine("Enter processing time of economy class ticket counters:");
                EconomyClassServiceRate = Input.InputText();
            }

         //get duration from console
            while (duration == 0)
            {
                Console.Out.WriteLine("Enter simulation duration in minutes:");
                duration = Input.InputText();
            }

            //create new clock(duration, arrival, service) named "clock"
            Clock clock = new Clock(duration, FirstClassArrivalRate, EconomyClassArrivalRate, FirstClassServiceRate, EconomyClassServiceRate);

            //initialize lists

            Globals.SetNumberFirstClassCounters(FirstClassCounters);
            Globals.SetNumberEconomyClassCounters(EconomyClassCounters);


            //create new first class ticket counters and place them in the firstClassStations array
            for (int i = 0; i < FirstClassCounters; i++)
            {
                TicketCounter newTicketCounter = new TicketCounter(FirstClassServiceRate, EconomyClassServiceRate);
                Globals.firstClassTicketCounters[i] = newTicketCounter;
            }
            //create 3 new economy class ticket counters and place them into the economyClassStations array
            for (int i = 0; i < EconomyClassCounters; i++)
            {
                TicketCounter newTicketCounter = new TicketCounter(FirstClassServiceRate, EconomyClassServiceRate);
                Globals.economyClassTicketCounters[i] = newTicketCounter;
            }

            //create queues
            Queue firstClassQueue = new Queue(FirstClassServiceRate);
            Queue economyQueue = new Queue(EconomyClassServiceRate);

            void PartA()
            {
                // empty buffers

                bufferAll.Clear();



                //find number of arriving first class passengers
                int numberFirstClass = HowManyPassengers.HowMany(FirstClassArrivalRate);

                //find number of arriving economy class passengers
                int numberEconomyClass = HowManyPassengers.HowMany(EconomyClassArrivalRate);

                //create  passengers and add to buffer (there may be more than one arriving so you need a buffer)

                for (int i = 0; i < numberFirstClass; i++)
                {
                    Passenger thisPassenger = new Passenger(true); //true means first class
                    bufferAll.Add(thisPassenger);
                }

                //add passengers from economy class to buffer
                for (int i = 0; i < numberEconomyClass; i++)
                {
                    Passenger thisPassenger = new Passenger(false);
                    bufferAll.Add(thisPassenger);
                }

                //shuffle all passengers in the buffer
                bufferAll.Shuffle();

                //for each new passenger
                foreach (Passenger passenger in bufferAll)
                {
                    //if passenger is first class
                    if (passenger.IsFirstClass == true)
                    {
                        //add the passenger to the first class queue

                        firstClassQueue.AddPassenger(passenger);

                    }
                    //else add passenger to economy queue
                    else
                    {
                        economyQueue.AddPassenger(passenger);

                    }



                }
                bufferAll.Clear();

            

            }

            void PartB()
            {
                //place passengers into stations
                firstClassQueue.PlacePassenger();
                economyQueue.PlacePassenger();

                //ask stations to process passengers
                for (int i = 0; i < Globals.firstClassTicketCounters.Length; i++)
                {
                    Globals.firstClassTicketCounters[i].ProcessPassenger();
                }

                for (int i = 0; i < Globals.economyClassTicketCounters.Length; i++)
                {
                    Globals.economyClassTicketCounters[i].ProcessPassenger();
                }



                //ask each passenger in the the two queues, and each passenger in the stations, to advance wait time.  Also advance counter uptime if applicable.

                foreach (Passenger passenger in firstClassQueue.passengers)
                {
                    passenger.AdvanceTimer();
                }

                foreach (Passenger passenger in economyQueue.passengers)
                {
                    passenger.AdvanceTimer();
                }

                foreach (TicketCounter counter in Globals.firstClassTicketCounters)
                {
                    if (counter.passenger[0] != null)
                    {
                        counter.AdvanceUptime();
                        counter.passenger[0].AdvanceTimer();
                    }

                }

                foreach (TicketCounter counter in Globals.economyClassTicketCounters)
                {
                    if (counter.passenger[0] != null)
                    {
                        counter.AdvanceUptime();
                        counter.passenger[0].AdvanceTimer();
                    }
                }

                //advance clock
                Console.Out.WriteLine("Clock: " + clock.Time);
                clock.Advance();
            }

            //While clock.time < clock.duration
            while (clock.Time < duration)
            {
                //run Part B
                PartA();
                PartB();

                Console.WriteLine("length of first class queue:");
                int lengthQueue = firstClassQueue.passengers.Count;
                Console.WriteLine(lengthQueue);
                Console.WriteLine("length of economy queue");
                lengthQueue = economyQueue.passengers.Count;
                Console.WriteLine(lengthQueue);
            }

            while ((firstClassQueue.passengers.Count > 0) || (economyQueue.passengers.Count > 0))
            {
                PartB();

                Console.WriteLine("length of first class queue:");
                int lengthQueue = firstClassQueue.passengers.Count;
                Console.WriteLine(lengthQueue);
                Console.WriteLine("length of economy queue");
                lengthQueue = economyQueue.passengers.Count;
                Console.WriteLine(lengthQueue);
            }

            int finishedLength = Globals.processedPassengers.Count;
            Console.WriteLine("finished passengers: " + finishedLength);

            Console.WriteLine("Maximum length first class queue: " + firstClassQueue.MaxLength + " passengers");

            Console.WriteLine("Maximum length coach class queue: " + economyQueue.MaxLength + " passengers");

            double awtFirstClass = 0;
            double awtEconomyClass = 0;

            double sumWaitTimeFirstClass = 0;
            double sumWaitTimeEconomyClass =0;
            double sumPassengersFirstClass =0;
            double sumPassengersEconomyClass = 0;

            int maximumWaitTimeFirstClass = 0;
            int maximumWaitTimeEconomyClass = 0;

            foreach (Passenger passenger in Globals.processedPassengers)
            {
                if (passenger.IsFirstClass == true)
                {
                    sumPassengersFirstClass += 1;
                    int waitTime = passenger.GetWaitTime();
                    sumWaitTimeFirstClass += waitTime;
                    if (waitTime > maximumWaitTimeFirstClass)
                    {
                        maximumWaitTimeFirstClass = waitTime;
                    }
                }
                else
                {
                    sumPassengersEconomyClass += 1;
                    int waitTime = passenger.GetWaitTime();
                    sumWaitTimeEconomyClass += waitTime;
                    if (waitTime > maximumWaitTimeEconomyClass)
                    {
                        maximumWaitTimeFirstClass = waitTime;
                    }
                }

            }

            awtFirstClass = sumWaitTimeFirstClass / sumPassengersFirstClass;
            awtEconomyClass = sumWaitTimeEconomyClass / sumPassengersEconomyClass;

            Console.WriteLine("Total running time: " + clock.Time);

            Console.WriteLine("Average wait time first class: " + awtFirstClass + " minutes" );
            Console.WriteLine("Average wait time coach class: " + awtEconomyClass + " minutes");

            Console.WriteLine("Maximum wait time first class: " + maximumWaitTimeFirstClass );
            Console.WriteLine("Maximum wait time coach class: " + maximumWaitTimeEconomyClass );

            for (int i = 0; i < Globals.firstClassTicketCounters.Length; i++)
                {
                double upTime = Globals.firstClassTicketCounters[i].uptime / Convert.ToDouble(clock.Time);
                Console.WriteLine("uptime" + Globals.firstClassTicketCounters[i].uptime);
                Console.WriteLine("First class ticket counter percent up time: " + upTime * 100 + "%");
                }
            for (int i = 0; i < Globals.economyClassTicketCounters.Length; i++)
            {
                double upTime = Globals.economyClassTicketCounters[i].uptime / Convert.ToDouble(clock.Time);
                Console.WriteLine("uptime: " + Globals.economyClassTicketCounters[i].uptime);
                Console.WriteLine("Coach class ticket counter percent up time: " + upTime * 100 + "%");
            }





            Console.ReadKey();

        }
    }
}
