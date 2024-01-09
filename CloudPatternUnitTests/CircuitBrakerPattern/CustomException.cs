namespace CloudPatternUnitTests.CircuitBrakerPattern
{
    public class CustomException: Exception
    {
        public CustomException(string message) :base(message){ }
    }
}
