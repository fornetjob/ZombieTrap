using Assets.Scripts.Core.Networking;
using Assets.Scripts.Core.Networking.Messages;

public class MessageService : IDependency
{
    private SerializerService _serializerService = null;

    public ConnectMessage ConvertToConnectMessage(MessageContract contract)
    {
        if (contract.Type != MessageType.Connect)
        {
            throw new System.ArgumentOutOfRangeException("type");
        }

        return _serializerService.Deserialize<ConnectMessage>(contract.Data);
    }
}