namespace CloudPatternUnitTests.CompetingConsumers
{
    public class MockMessageQueue
    {
        private readonly Queue<string> _messages = new Queue<string>();
        private readonly HashSet<string> _unacknowledgedMessages = new HashSet<string>();

        public void Enqueue(string message)
        {
            lock (_messages)
            {
                _messages.Enqueue(message);
            }
        }

        public string Dequeue()
        {
            lock (_messages)
            {
                if(_messages.Count > 0)
                {
                    var message = _messages.Dequeue();
                    _unacknowledgedMessages.Add(message);
                    return message;
                }
                return null;
            }
        }

        public void Acknowledge(string message)
        {
            lock(_unacknowledgedMessages)
            {
                _unacknowledgedMessages.Remove(message);
            }
        }

        public int UnacknowledgeMessagesCount => _unacknowledgedMessages.Count;
    }
}
