using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3
{
    public static class HowManyPassengers
    {
        static private int numberPassengers = 0;

        public static int HowMany(int rate)
        {
            numberPassengers = 0;
            for (int i = 1; i < 650; i++)
            {
                Random randomGen = new Random();
                double random = randomGen.NextDouble();
                //the probability of one passenger every 6 minutes is 1/7, the probability of two passengers is 1/49... etc
                double test = (1 / (Math.Pow((rate + 1), i)));
                if (test > random)
                {
                    numberPassengers = i;              
                }
                else
                {
                    continue;
                }
            }
           // Console.WriteLine("This many passengers arriving:")
           // Console.WriteLine(numberPassengers);
            return numberPassengers;
        }
        




    }
}
