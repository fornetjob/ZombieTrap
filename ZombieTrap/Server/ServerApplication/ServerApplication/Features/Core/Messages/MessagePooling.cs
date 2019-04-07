using Game.Core.Networking;
using ServerApplication.Features.Core.Messages;
using System;

public class MessagePooling : IDependency
{
    #region Fields

    private WeakDictionary<Guid, MessageQueue>
        _messageDict = new WeakDictionary<Guid, MessageQueue>(() => new MessageQueue());

    private WeakDictionary<Guid, MessageQueue>
        _strongMessageDict = new WeakDictionary<Guid, MessageQueue>(() => new MessageQueue());

    #endregion

    #region Public methods

    public void AddMessage(Guid playerId, MessageContract msg)
    {
        if (msg.Type.IsStrongMessage())
        {
            _strongMessageDict[playerId].Enqueue(msg);
        }
        else
        {
            _messageDict[playerId].Enqueue(msg);
        }
    }

    public MessageQueue GetMessageQueue(Guid playerId)
    {
        return _messageDict[playerId];
    }

    public MessageQueue GetStrongMessageQueue(Guid playerId)
    {
        return _strongMessageDict[playerId];
    }

    #endregion
}