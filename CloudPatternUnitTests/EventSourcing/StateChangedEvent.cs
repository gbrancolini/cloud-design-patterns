namespace CloudPatternUnitTests.EventSourcing
{
    public class StateChangedEvent : IEvent
    {
        public int EntityId{get; private set;}
        public string NewState { get;private set;}

        public StateChangedEvent(int entityId, string newState)
        {
            EntityId = entityId;
            NewState = newState;
        }
    }
}
