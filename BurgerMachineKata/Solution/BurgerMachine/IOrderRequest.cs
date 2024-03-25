using System;
using System.Collections.Generic;
using System.Text;

namespace BurgerMachine
{
    public interface IOrderRequest
    {
        string GetTerminalCode();
    }
}
