using Game.Core.Networking;
using System.Collections.Generic;

public class SenderMessagesPooling:IDependency
{
    private object _lockObj = new object();

    private Queue<MessageContract> 
        _queue = new Queue<MessageContract>();

    public void Clear()
    {
        lock (_lockObj)
        {
            _queue.Clear();
        }
    }

    public bool IsHasMessages()
    {
        lock (_lockObj)
        {
            return _queue.Count > 0;
        }
    }

    public void Enqueue(MessageContract msg)
    {
        lock (_lockObj)
        {
            _queue.Enqueue(msg);
        }
    }

    public MessageContract Dequeue()
    {
        lock (_lockObj)
        {
            return _queue.Dequeue();
        }
    }
}