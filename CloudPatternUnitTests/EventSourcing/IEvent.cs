namespace CloudPatternUnitTests.EventSourcing
{
    public interface IEvent
    {
        int EntityId { get; }
    }
}
