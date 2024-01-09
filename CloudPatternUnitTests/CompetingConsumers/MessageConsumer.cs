namespace CloudPatternUnitTests.CompetingConsumers
{
    public class MessageConsumer
    {
        public int ProcessedMessageCount { get; private set; } = 0;

        public void ProcessMessage(MockMessageQueue messageQueue)
        {
            string message = string.Empty;
            while((message = messageQueue.Dequeue()) != null)
            {
                // Simulate message processing
                ProcessedMessageCount++;

                // Acknowledge the message
                messageQueue.Acknowledge(message);
            }
        }
    }
}
