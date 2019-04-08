using ServerApplication.Features.Players;
using System;
using System.Collections.Generic;

public class PlayersPooling:IDependency
{
    private Dictionary<Guid, Player>
        _dict = new Dictionary<Guid, Player>();

    private WeakDictionary<Guid, List<Player>>
        _roomDict = new WeakDictionary<Guid, List<Player>>(()=>new List<Player>());

    private List<Player>
        _players = new List<Player>();

    public int GetPlayerCount(Guid roomId)
    {
        return _roomDict[roomId].Count;
    }

    public void AddPlayer(Player player)
    {
        _dict.Add(player.PlayerId, player);
        _roomDict[player.RoomId].Add(player);

        _players.Add(player);
    }

    public List<Player> GetPlayers()
    {
        return _players;
    }

    public Player GetPlayer(Guid playerId)
    {
        return _dict[playerId];
    }

    public bool IsExistPlayer(Guid playerId)
    {
        return _dict.ContainsKey(playerId);
    }
}