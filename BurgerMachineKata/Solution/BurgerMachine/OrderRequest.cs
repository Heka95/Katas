using System;
using System.Collections.Generic;
using System.Text;
using BurgerMachineBoard;

namespace BurgerMachine
{
    public class OrderRequest :IOrderRequest
    {
        private readonly IOrder _order;

        public OrderRequest(IOrder order)
        {
            _order = order;
        }

        public string GetTerminalCode()
        {
            return _order.GetCode();
        }
    }
}
