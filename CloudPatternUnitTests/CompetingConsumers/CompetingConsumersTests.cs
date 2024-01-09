namespace CloudPatternUnitTests.CompetingConsumers
{
    /// <summary>
    /// Enable multiple concurrent consumers to process messages received on the same messaging channel. 
    /// This pattern enables a system to process multiple messages concurrently to optimize throughput, 
    /// to improve scalability and availability, and to balance the workload.
    /// </summary>
    public class CompetingConsumersTests
    {
        /// <summary>
        /// This test demonstrate the basic usage of this pattern. Keep in mind that the following frameworks and technologies are being used
        /// nowadays into distribuited systems to use this pattern:
        /// RabbitMQ, Apache Kafka, Amazon SQS (Simple Queue Service), Azure Service Bus, ActiveMQ, Ms Message Queuing (MSMQ), Google Cloud Pub/Sub,
        /// Redis Streams, IBM MQ.
        /// </summary>
        [Fact]
        public void MultipleConsumerShouldProcessAndAcknowledgeMessagesConcurrently()
        {
            var queue = new MockMessageQueue();
            var consumers = new List<MessageConsumer> { new MessageConsumer(), new MessageConsumer() };
            int totalToBeProcessed = 10;

            // Enqueue some messages
            for(int i = 0; i < totalToBeProcessed; i++)
            {
                queue.Enqueue($"Message {i}");
            } 

            // Start consumers as separate tasks
            var consumerTasks = consumers.Select(consumer => Task.Run(() => { consumer.ProcessMessage(queue); })).ToList();

            // Wait for all consumers to finish processing
            Task.WhenAll(consumerTasks).Wait();

            // Assert that messages were processed
            int totalProcessed = consumers.Sum(consumer => consumer.ProcessedMessageCount);
            Assert.Equal(totalToBeProcessed, totalProcessed);
            Assert.Equal(0, queue.UnacknowledgeMessagesCount);
        }
    }
}
