using Assets.Scripts.Core.Networking;
using Assets.Scripts.Features.Core.Networking.Messages;

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

    public PositionsMessage ConvertToPositionsMessage(MessageContract contract)
    {
        if (contract.Type != MessageType.Positions)
        {
            throw new System.ArgumentOutOfRangeException("type");
        }

        return _serializerService.Deserialize<PositionsMessage>(contract.Data);
    }
}