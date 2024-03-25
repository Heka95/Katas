namespace BurgerMachineBoard
{
    public class Order : IOrder
    {
        private readonly string _code;

        public Order(string code)
        {
            _code = code;
        }

        public string GetCode()
        {
            return _code;
        }
    }
}
