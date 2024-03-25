using BurgerMachineBoard;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace BurgerMachine.Tests
{
    public class OrderRequestTest
    {
        [Test]
        public void ShouldCallGetCodeWhenGeTerminalCode()
        {
            // Given
            var order = Substitute.For<IOrder>();
            IOrderRequest orderRequest = new OrderRequest(order);

            // When
            orderRequest.GetTerminalCode();

            // Then
            order.Received().GetCode();
        }

        [TestCase("B")]
        [TestCase("F")]
        [TestCase("C")]
        public void ShouldReturnCodeFromOrder(string expectedCode)
        {
            // Given
            var order = Substitute.For<IOrder>();
            order.GetCode().Returns(expectedCode);
            IOrderRequest orderRequest = new OrderRequest(order);

            // When
            var orderCode = orderRequest.GetTerminalCode();

            // Then
            orderCode.Should().Be(expectedCode);
        }
    }
}