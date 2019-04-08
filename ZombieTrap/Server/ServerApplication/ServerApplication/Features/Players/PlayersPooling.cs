using ServerApplication.Features.Players;
using System;
using System.Collections.Generic;
using System.Net;

public class PlayersPooling:IDependency
{
    private Dictionary<Guid, Player>
        _dict = new Dictionary<Guid, Player>();

    private WeakDictionary<Guid, List<Player>>
        _roomDict = new WeakDictionary<Guid, List<Player>>(()=>new List<Player>());

    private List<Player>
        _players = new List<Player>();

    public List<Player> GetRoomPlayers(Guid roomId)
    {
        return _roomDict[roomId];
    }

    public void AddPlayer(Player player)
    {
        _dict.Add(player.PlayerId, player);
        _roomDict[player.RoomId].Add(player);

        _players.Add(player);
    }

    public void RemovePlayer(Player player)
    {
        _dict.Remove(player.PlayerId);
        _roomDict[player.RoomId].Remove(player);

        _players.Remove(player);
    }

    public List<Player> GetPlayers()
    {
        return _players;
    }

    public Player GetPlayer(Guid playerId)
    {
        return _dict[playerId];
    }

    public Player GetPlayer(IPEndPoint endPoint)
    {
        var endpointStr = endPoint.ToString();

        for (int i = 0; i < _players.Count; i++)
        {
            var player = _players[i];

            if (player.EndPoint.ToString() == endpointStr)
            {
                return player;
            }
        }

        return null;
    }
}