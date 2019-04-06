using Assets.Scripts.Core.Networking;
using Assets.Scripts.Core.Networking.Messages;
using System;

public class MessageFactory : IDependency
{
    private SerializerService _serializerService = null;

    public MessageContract CreateConnectMessage(Guid playerIdentity)
    {
        var connectMessage = new ConnectMessage
        {
            PlayerId = playerIdentity
        };

        return new MessageContract
        {
            Type = MessageType.Connect,
            Data = _serializerService.Serialize(connectMessage)
        };
    }
}