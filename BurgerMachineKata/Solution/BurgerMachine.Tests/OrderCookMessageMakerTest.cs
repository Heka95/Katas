using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace BurgerMachine.Tests
{
    public class OrderCookMessageMakerTest
    {
        [Test]
        public void ShouldBuildEmptyMessageWhenReceiveEmptyOrderRequest()
        {
            // Given
            IOrderRequest orderRequest = Substitute.For<IOrderRequest>();
            orderRequest.GetTerminalCode().Returns(string.Empty);
            IOrderCookMessageMaker orderCookMessageMaker = new OrderCookMessageMaker();

            // When
            string expectedMessage = orderCookMessageMaker.CreateMessageToCooks(orderRequest);

            // Then
            expectedMessage.Should().BeEmpty();
        }

        [TestCase("N-N-N", "")]
        [TestCase("B-C-F", "Burger Menu")]
        [TestCase("C-F-C", "CheeseBurger Menu")]
        [TestCase("F-F-C", "FishBurger Menu")]
        public void ShouldBuildMenuMessageWhenReceiveMenuCodes(string expectedCode, string expectedResult)
        {
            // Given
            IOrderRequest orderRequest = Substitute.For<IOrderRequest>();
            orderRequest.GetTerminalCode().Returns(expectedCode);
            IOrderCookMessageMaker orderCookMessageMaker = new OrderCookMessageMaker();

            // When
            string expectedMessage = orderCookMessageMaker.CreateMessageToCooks(orderRequest);

            // Then
            expectedMessage.Should().Be(expectedResult);
        }

        [TestCase("B-N-N", "Burger")]
        [TestCase("N-F-C", "Fries and Cola")]
        [TestCase("F-N-C", "FishBurger and Cola")]
        public void ShouldBuildCustomMessageWhenReceiveCustomCodes(string expectedCode, string expectedResult)
        {
            // Given
            IOrderRequest orderRequest = Substitute.For<IOrderRequest>();
            orderRequest.GetTerminalCode().Returns(expectedCode);
            IOrderCookMessageMaker orderCookMessageMaker = new OrderCookMessageMaker();

            // When
            string expectedMessage = orderCookMessageMaker.CreateMessageToCooks(orderRequest);

            // Then
            expectedMessage.Should().Be(expectedResult);
        }
    }
}
