namespace CloudPatternUnitTests.EventSourcing
{
    public class EventSourcingTests
    {
        [Fact]
        public void EntityShouldRestoreStateFromEvents()
        {
            var eventStore = new EventStore();
            int entityId = 1;

            eventStore.AddEvent(new StateChangedEvent(entityId, "Created"));
            eventStore.AddEvent(new StateChangedEvent(entityId, "Updated"));

            var events = eventStore.GetEvents(entityId);
            var entity = new Entity(events);

            Assert.Equal("Updated", entity.State);
        }
    }
}
