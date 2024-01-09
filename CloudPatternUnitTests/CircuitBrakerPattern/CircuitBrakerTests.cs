using Polly;
using Polly.CircuitBreaker;

namespace CloudPatternUnitTests.CircuitBrakerPattern
{
    /// <summary>
    /// Handle faults that may take a variable amount of time to rectify when connecting to a 
    /// remote service or resource. This pattern can improve the stability and resiliency of an application.
    /// </summary>
    public class CircuitBrakerTests
    {
        private int durationOfBreak = 1;

        private AsyncCircuitBreakerPolicy CreateCircuitBrakerPolicy()
        {
            unsafe
            {
                return Policy
                    .Handle<Exception>()
                    .CircuitBreakerAsync(
                        exceptionsAllowedBeforeBreaking: 2, durationOfBreak: TimeSpan.FromSeconds(durationOfBreak));
            }
        }

        private AsyncPolicy<string> CreateCircuitBrakerWithFallbackPolicy()
        {
            var circuitBrakerPolicy = CreateCircuitBrakerPolicy();

            var fallbackPolicy = Policy<string>.Handle<Exception>()
                                .FallbackAsync(
                                        fallbackValue: "Fallback Value", 
                                        onFallbackAsync: async exception => 
                                        { 
                                            await Task.CompletedTask; 
                                        });

            return fallbackPolicy.WrapAsync(circuitBrakerPolicy);
        }

        private AsyncCircuitBreakerPolicy CreateCircuitBreakerPolicyForSpecificException()
        {
            return Policy
                    .Handle<CustomException>()
                    .CircuitBreakerAsync(
                        exceptionsAllowedBeforeBreaking: 1,
                        durationOfBreak: TimeSpan.FromSeconds(durationOfBreak));
        } 


        [Fact]
        public async Task CircuitShouldBeClosedInitially()
        {
            var circuitBrakerPolicy = CreateCircuitBrakerPolicy();

            Assert.Equal(CircuitState.Closed, circuitBrakerPolicy.CircuitState);
        }

        

        


        [Fact]
        public async Task ShouldreturnFallbackValueOnFailure()
        {
            var circuitBrakerPolicy = CreateCircuitBrakerWithFallbackPolicy();
            var service = new CircuitBrakerFakeService();

            var result = await circuitBrakerPolicy.ExecuteAsync(() => service.UnreliableMethod());

            Assert.Equal("Fallback Value", result);
        }

        [Fact]
        public async Task CircuitShouldBreakOnSpecificException()
        {
            var circuitBraker = CreateCircuitBreakerPolicyForSpecificException();

            await Assert.ThrowsAsync<CustomException>(() => circuitBraker.ExecuteAsync(()=> throw new CustomException("Specific Error")));

            Assert.Equal(CircuitState.Open, circuitBraker.CircuitState);
        }

        [Fact]
        public async Task CircuitShouldNotbreakOnOtherExceptions()
        {
            var circuitBraker = CreateCircuitBreakerPolicyForSpecificException();

            await Assert.ThrowsAsync<InvalidOperationException>(() => circuitBraker.ExecuteAsync(() => throw new InvalidOperationException("Specific Error")));

            Assert.Equal(CircuitState.Closed, circuitBraker.CircuitState);
        }
    }
}
