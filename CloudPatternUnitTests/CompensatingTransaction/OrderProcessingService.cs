namespace CloudPatternUnitTests.CompensatingTransaction
{
    public class OrderProcessingService
    {
        private readonly OrderService _orderService;
        private readonly PaymentService _paymentService;

        public OrderProcessingService(OrderService orderService, PaymentService paymentService)
        {
            _orderService = orderService;
            _paymentService = paymentService;
        }

        public bool TryProcessOrder()
        {
            var orderId = _orderService.CreateOrder();

            if (!_paymentService.ProgressPayment())
            {
                _orderService.Cancelorder(orderId); 
                return false;
            }

            return true;
        }
    }
}
