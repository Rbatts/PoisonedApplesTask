using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PoisonedApples
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = 10000;
            var apples1 = PickApples().Take(items).ToList();
            WriteToConsole(apples1);
            Console.ReadLine();

            var apples = PickApples().Take(items).Count(apple => apple.Poisoned);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n Poisoned Apples : {apples}");
            Console.ResetColor();
            Console.ReadLine();

            var poisonedApples = PickApples().Take(items).Where(apple => apple.Colour != "Red")
                .GroupBy(apple => apple.Colour).OrderByDescending(group => group.Count()).First().Key;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($" Next Most Poisionous Apple Colour After Red : {poisonedApples}");
            Console.ResetColor();
            Console.ReadLine();

            var pickRedApplesInARow = PickApples().Take(items).Aggregate((Current: 0, Max: 0),
                (acc, apple) => apple.Colour == "Red" && !apple.Poisoned ? (acc.Current + 1, Math.Max(acc.Current + 1, acc.Max)) : (0, acc.Max)).Item2;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($" Consecutive Clean Red Apples Picked in a Row : {pickRedApplesInARow}");
            Console.ResetColor();
            Console.ReadLine();

            var consecutiveGreenApples = PickApples().Take(items)
                .Zip(PickApples().Skip(1), (a, b) => a.Colour == "Green" && b.Colour == "Green").Where(apple => apple).ToList().Count;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($" Green Apple Pairs Picked : {consecutiveGreenApples}");
            Console.ResetColor();
            Console.ReadLine();

            //var redPoisonedApples = poisonedApples.Where(i => i == "Red").ToList().Count;
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine($"\n Red Poisoned Apple Count : {redPoisonedApples}");
            //Console.ResetColor();

            //var greenPoisonedApples = poisonedApples.Where(i => i == "Green").ToList().Count;
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine($"\n Green Poisoned Apple Count : {greenPoisonedApples}");
            //Console.ResetColor();

            //var yellowPoisonedApples = poisonedApples.Where(i => i == "Yellow").ToList().Count;
            //Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.WriteLine($"\n Yellow Poisoned Apple Count : {yellowPoisonedApples} ");
            //Console.ResetColor();

        }

        public static string GetColour(int colourIndex)
        {
            if (colourIndex % 13 == 0 || colourIndex % 29 == 0)
            {
                return "Green";
            }

            if (colourIndex % 11 == 0 || colourIndex % 19 == 0)
            {
                return "Yellow";
            }

            return "Red";

        }

        public static IEnumerable<Apple> PickApples()
        {
            int colourIndex = 1;
            int poisonIndex = 7;

            while (true)
            {
                yield return new Apple
                {
                    Colour = GetColour(colourIndex),
                    Poisoned = poisonIndex % 41 == 0
                };

                colourIndex += 5;
                poisonIndex += 37;
            }
        }

        private static void WriteToConsole(List<Apple> items)
        {
            int index = 0;
            for (int i = 0; i < items.Count; i++)
            {
                index = index + 1;
                Console.WriteLine($"{index} - {items[i]}");
            }
        }
    }
}
