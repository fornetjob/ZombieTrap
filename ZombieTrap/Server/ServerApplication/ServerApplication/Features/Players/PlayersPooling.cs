using ServerApplication.Features.Players;
using System;
using System.Collections.Generic;

public class PlayersPooling:IDependency
{
    private Dictionary<Guid, Player>
        _dict = new Dictionary<Guid, Player>();

    private List<Player>
        _players = new List<Player>();

    public void AddPlayer(Player player)
    {
        _dict.Add(player.PlayerId, player);
        _players.Add(player);
    }

    public List<Player> GetPlayers()
    {
        return _players;
    }

    public bool IsExistPlayer(Guid playerId)
    {
        return _dict.ContainsKey(playerId);
    }
}