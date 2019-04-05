using Assets.Scripts.Core.Networking;
using Assets.Scripts.Core.Networking.Messages;
using System;
using System.IO;

public class MessageFactory : IDependency
{
    private MemoryStream
        _stream = new MemoryStream();

    public MessageContract CreateConnectMessage(Guid playerIdentity)
    {
        var connectMessage = new ConnectMessage
        {
            PlayerId = playerIdentity
        };

        return new MessageContract
        {
            Type = MessageType.Connect,
            Data = GetBytes(connectMessage)
        };
    }

    private byte[] GetBytes<T>(T msg)
    {
        _stream.Position = 0;
        _stream.SetLength(0);

        ProtoBuf.Serializer.Serialize(_stream, msg);

        return _stream.ToArray();
    }
}