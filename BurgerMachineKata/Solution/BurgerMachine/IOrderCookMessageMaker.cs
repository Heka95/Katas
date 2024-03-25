using System;
using System.Collections.Generic;
using System.Text;

namespace BurgerMachine
{
    public interface IOrderCookMessageMaker
    {
        string CreateMessageToCooks(IOrderRequest orderRequest);
    }
}
