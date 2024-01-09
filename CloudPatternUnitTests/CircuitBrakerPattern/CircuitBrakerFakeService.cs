namespace CloudPatternUnitTests.CircuitBrakerPattern
{
    public class CircuitBrakerFakeService
    {
        public Task<string> UnreliableMethod()
        {
            throw new Exception("Operation fail");
        }

        public Task<string> FallbackMethod()
        {
            return Task.FromResult("Fallback operation");
        }
    }
}
