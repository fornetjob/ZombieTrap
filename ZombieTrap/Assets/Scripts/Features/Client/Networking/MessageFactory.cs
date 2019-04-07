using Assets.Scripts.Core.Networking;
using Assets.Scripts.Core.Networking.Messages;

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
            Positions = new Vector2[zombies.Length]
        };

        for (int i = 0; i < zombies.Length; i++)
        {
            var zombie = zombies[i];

            msg.Identities[i] = zombie.identity.value;
            msg.Positions[i] = zombie.position.value;
        }

        return msg;
    }
}