using System.IO;
using System.Linq;

namespace BurgerMachine
{
    public class DetailedOrder
    {
        private const string DisplaySeparator = " and ";
        public string Sandwich { get; set; }
        public string Side { get; set; }
        public string Drink { get; set; }

        public string Display()
        {
            var valueToDisplay = string.Join(DisplaySeparator, new[] {Sandwich, Side, Drink}.Where(c => !string.IsNullOrEmpty(c)));
            return valueToDisplay;
        }
    }
}