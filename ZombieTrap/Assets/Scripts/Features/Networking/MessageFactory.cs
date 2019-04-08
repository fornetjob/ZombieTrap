using Game.Core.Networking;
using Game.Core.Networking.Messages;

using System;

public class MessageFactory : IDependency
{
    #region Poolings

    private SenderMessagesPooling _senderMessagesPooling = null;

    #endregion

    #region Services

    private NetworkSettingsService _networkSettingsService = null;
    private SerializerService _serializerService = null;

    #endregion

    #region Fields

    private Guid
        _playerId = Guid.NewGuid();

    #endregion

    public void CreateConnectMessage()
    {
        var msg = new MessageContract
        {
            Type = MessageType.Connect,
            Data = _serializerService.Serialize(new ConnectMessage
            {
                PlayerId = _playerId,
                Port = new PortInterval
                {
                    port = _networkSettingsService.GetListenConfiguration().ListeningPort
                },
            })
        };

        _senderMessagesPooling.Enqueue(msg);
    }

    public void CreateReplyMessage(ulong msgId)
    {
        var msg = new MessageContract
        {
            Id = msgId,
            Type = MessageType.Reply,
            Data = _serializerService.Serialize(new ReplyMessage
            {
                PlayerId = _playerId
            })
        };

        _senderMessagesPooling.Enqueue(msg);
    }
}