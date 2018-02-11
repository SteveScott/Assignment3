using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3
{
    public static class Input
    {
        public static int InputText()
        {
            int duration = 0;
            string input = Console.ReadLine();
            try
            {
                duration = int.Parse(input);
            }

            catch
            {
                Console.Out.WriteLine("You must enter a whole number greater than 0");
                return 0;
            }
            if (duration < 0) { Console.Out.WriteLine("Must be greater than 0"); return 0; }
            return duration;
        }
    }
}
