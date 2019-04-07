using Assets.Scripts.Core.Networking.Udp;
using ServerApplication.Features.Players;

using System;
using System.Net;

public class PlayerFactory : IDependency
{
    private PlayersPooling _playersPooling = null;

    public Player Create(Guid playerId, IPEndPoint endPoint)
    {
        var player = new Player
        {
            PlayerId = playerId,
            Sender = new UdpSender(new SendConfiguration
            {
                EndPoint = endPoint
            })
        };

#if DEBUG
        System.Console.WriteLine("Connected player {0}", playerId);
#endif

        _playersPooling.AddPlayer(player);

        return player;
    }
}