using Game.Core.Networking;
using Game.Core.Networking.Messages;

public class MessageService : IService
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

    public ReplyMessage ConvertToReplyMessage(MessageContract contract)
    {
        if (contract.Type != MessageType.Reply)
        {
            throw new System.ArgumentOutOfRangeException("type");
        }

        return _serializerService.Deserialize<ReplyMessage>(contract.Data);
    }

    public RoomMessage ConvertToRoomMessage(MessageContract contract)
    {
        if (contract.Type != MessageType.Room)
        {
            throw new System.ArgumentOutOfRangeException("type");
        }

        return _serializerService.Deserialize<RoomMessage>(contract.Data);
    }

    public ItemsMessage ConvertToItemsMessage(MessageContract contract)
    {
        if (contract.Type != MessageType.Items)
        {
            throw new System.ArgumentOutOfRangeException("type");
        }

        return _serializerService.Deserialize<ItemsMessage>(contract.Data);
    }

    public PositionsMessage ConvertToPositionsMessage(MessageContract contract)
    {
        if (contract.Type != MessageType.Positions)
        {
            throw new System.ArgumentOutOfRangeException("type");
        }

        return _serializerService.Deserialize<PositionsMessage>(contract.Data);
    }

    public DamageMessage ConvertToDamageMessage(MessageContract contract)
    {
        if (contract.Type != MessageType.Damage)
        {
            throw new System.ArgumentOutOfRangeException("type");
        }

        return _serializerService.Deserialize<DamageMessage>(contract.Data);
    }
}