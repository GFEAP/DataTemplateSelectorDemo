using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateSelectorDemo.MockData
{
  public class MockDataGenerator
    {
        public static MockDataGenerator Default = new MockDataGenerator();
        public MockDataGenerator()
        {
            RandomGenerator = new Random();
        }
        private Random RandomGenerator { get; set; }

        private readonly string[] FirstNames = new string[] { "Filmore", "Harry", "Peter", "Never" };
        private readonly string[] MiddleNames = new string[] { "A.", "B.", "C.", "X." };
        private readonly string[] LastNames = new string[] { "Fish", "Hog", "Pelican", "Raven" };
        private readonly string[] UOM = new string[] { "m", "mm", "cm", "µ", "ft", "ml", "pints", "miles", "in", "Angstrom" };

        public string GetName
        {
            get
            {
                var a = RandomGenerator.Next(0, 100);
                string first = FirstNames[a%FirstNames.Length];

                a = RandomGenerator.Next(0, 100);
                string middle = MiddleNames[a%MiddleNames.Length];

                RandomGenerator.Next(0, 100);
                string last = LastNames[a%LastNames.Length];

                return $"{first} {middle} {last}";
            }
        }
        public string GetUom => UOM[RandomGenerator.Next(0, 100)%UOM.Length];
        public double GetQuantity => RandomGenerator.NextDouble() * 100;
        public int GetCounter
        {
            get => RandomGenerator.Next(0, 100);
        }
    }
}