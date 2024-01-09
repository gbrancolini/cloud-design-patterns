namespace CloudPatternUnitTests.EventSourcing
{
    public class Entity
    {
        public int Id { get;private set; }
        public string State { get; private set; } 

        public Entity(IEnumerable<IEvent> events)
        {
            foreach (var @event  in events)
            {
                Apply(@event);
            }
        }

        public void Apply(IEvent @event)
        {
            switch (@event)
            {
                case StateChangedEvent stateChanged:
                    State = stateChanged.NewState; 
                    break;
            }
        }
    }
}
