using Game.Core.Networking;
using Game.Core.Networking.Messages;

using System;
using UnityEngine;

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

    public PositionsMessage CreatePositionsMessage(GameEntity[] zombies)
    {
        var msg = new PositionsMessage
        {
            Identities = new ulong[zombies.Length],
            Positions = new Game.Core.Vector2Float[zombies.Length]
        };

        for (int i = 0; i < zombies.Length; i++)
        {
            var zombie = zombies[i];

            msg.Identities[i] = zombie.identity.value;
            msg.Positions[i] = new Game.Core.Vector2Float(zombie.position.value.x, zombie.position.value.y);
        }

        return msg;
    }
}