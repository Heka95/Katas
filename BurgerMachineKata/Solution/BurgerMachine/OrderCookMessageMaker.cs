using System.Collections.Generic;

namespace BurgerMachine
{
    public class OrderCookMessageMaker : IOrderCookMessageMaker
    {
        private const string NullCode = "N-N-N";

        private readonly Dictionary<string, string> _codeTransformDictionary = new Dictionary<string, string>
        {
            { "B-C-F", "Burger Menu" },
            { "C-F-C", "CheeseBurger Menu" },
            { "F-F-C", "FishBurger Menu" }
        };

        public string CreateMessageToCooks(IOrderRequest orderRequest)
        {
            var orderCode = orderRequest.GetTerminalCode();

            if (string.IsNullOrEmpty(orderCode) || orderCode.Equals(NullCode))
            {
                return string.Empty;
            }

            if (IsMenu(orderCode))
            {
                return _codeTransformDictionary[orderCode];
            }
            else
            {
                var parser = new OrderParser();
                var detailedOrder = parser.GetDetailedOrder(orderCode);
                return detailedOrder.Display();
            }
        }

        private bool IsMenu(string orderCode)
        {
            return !orderCode.Contains("N");
        }
    }
}
