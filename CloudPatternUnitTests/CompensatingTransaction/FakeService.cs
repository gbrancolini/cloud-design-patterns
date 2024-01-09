namespace CloudPatternUnitTests.CompensatingTransaction
{
    public class FakeService
    {
        public bool OperationA(bool isSuccesfull) { return isSuccesfull; }

        public bool OperationB(bool isSuccesfull) { return isSuccesfull; }

        public void CompensationOperationA() { }
    }

    public class OrderService
    {
        public int CreateOrder()
        {
            return new Random().Next(1000, 9999);
        }

        public void Cancelorder(int orderId) { }
    }

    public class PaymentService
    {
        public bool ProgressPayment() { 
            return false; //set to false to simulate payment failure.
        }
    }
}
