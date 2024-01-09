namespace CloudPatternUnitTests.CompensatingTransaction
{
    /// <summary>
    /// Undo the work performed by a series of steps, which together define an eventually consistent operation, 
    /// if one or more of the operations fails. Operations that follow the eventual consistency model are commonly 
    /// found in cloud-hosted applications that implement complex business processes and workflows.
    /// </summary>
    public class CompensatingTransactionTests
    {
        [Fact]
        public void ShouldCompensateOperationAIfOperationBFails()
        {
            var fakeService = new FakeService();

            bool operationASuccess = fakeService.OperationA(true);
            Assert.True(operationASuccess, "Operation A should success");

            bool operationBSuccess = fakeService.OperationB(false);

            if(!operationBSuccess)
            {
                fakeService.CompensationOperationA();

            }
            Assert.False(operationBSuccess, "Operation B should fail triggering compensation.");
        }

        [Fact]
        public void ShouldCompensateWhenPaymentFalls()
        {
            var orderService = new OrderService();
            var paymentService=  new PaymentService();
            var orderProcessingService = new OrderProcessingService(orderService, paymentService);

            bool success = orderProcessingService.TryProcessOrder();

            Assert.False(success, "Order processing should fails due to payment.");
        }
    }
}
