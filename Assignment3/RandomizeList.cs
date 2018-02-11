using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3
{
    public static class ShuffleClass
    {
        //"Fisher-Yates shuffle: https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void ShuffleArray(this Object[] thisArray)
        {
            int n = thisArray.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                object thisItem = thisArray[k];
                thisArray[k] = thisArray[n];
                thisArray[n] = thisItem;

            }

        }

    }
}
