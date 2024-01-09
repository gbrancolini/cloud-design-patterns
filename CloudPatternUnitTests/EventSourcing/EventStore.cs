namespace CloudPatternUnitTests.EventSourcing
{
    /// <summary>
    /// This simulates a simple in memory store. The real ones commonly used are Axon Framework (java), EventStore, Apache Kafka, Redis Streams, RabbitMQ, 
    /// Ms Orleans and Akka (scala and java)
    /// </summary>
    public class EventStore 
    {
        private readonly List<IEvent> _events = new List<IEvent>();

        public void AddEvent(IEvent eventItem) => _events.Add(eventItem);

        public IEnumerable<IEvent> GetEvents(int entityId) => _events.Where(e => e.EntityId == entityId);
    
    }
}
