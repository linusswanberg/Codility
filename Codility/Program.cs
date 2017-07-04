namespace Codility
{
    using System;
    using System.Collections.Generic;

    internal class Program
    {
        private static void Main()
        {
            // This array should result in 16 strokes.
            int[] houses = { 10, 2, 8, 4 };

            Console.WriteLine($" \n The number of horizontal strokes: {CalculatedTheAmountOfStrokes(houses)}");
            Console.ReadKey();
        }
        private static int CalculatedTheAmountOfStrokes(IEnumerable<int> houses)
        {
            // The number of max strokes, should return -1 if it exceedes.
            const int maxStrokes = 1000000000;
            int strokes = 0, skyline = 0;

            foreach (var house in houses)
            {
                // Checks if the House is higher then the skyline, initial value is 0.
                if (house > skyline)
                {
                    strokes += house - skyline;
                    skyline = house;

                    // Checks if maxStrokes is exeeded in every iteration, for efficiency.
                    if (strokes > maxStrokes)
                    {
                        return -1;
                    }
                }
                else if (house < skyline)
                {
                    skyline = house;
                }
            }
            return strokes;
        }
    }
}
