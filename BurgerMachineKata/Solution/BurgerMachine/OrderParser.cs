using System.Collections.Generic;

namespace BurgerMachine
{
    public class OrderParser
    {
        public Dictionary<string, string> SandwichDictionary = new Dictionary<string, string>
            {
                { "B", "Burger" },
                { "C", "CheeseBurger" },
                { "F", "FishBurger" }
            };

        public DetailedOrder GetDetailedOrder(string orderCode)
        {
            var detailedOrder = new DetailedOrder();
            var orderPart = orderCode.Split('-');
            detailedOrder.Sandwich = orderPart[0] == "N" ? string.Empty : SandwichDictionary[orderPart[0]];
            detailedOrder.Side = orderPart[1] == "N" ? string.Empty : "Fries";
            detailedOrder.Drink = orderPart[2] == "N" ? string.Empty : "Cola";
            return detailedOrder;
        }
    }
}
