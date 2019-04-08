using ServerApplication.Features.Items;
using System.Collections.Generic;

public class ProjectilePooling : IDependency
{
    #region Factories

    private MessageFactory _messageFactory = null;

    #endregion

    private List<Item> _projectiles = new List<Item>();

    public void Add(Item projectile)
    {
        _projectiles.Add(projectile);

        _messageFactory.CreateItemMessage(projectile.RoomId, projectile);
    }

    public void Remove(Item projectile)
    {
        _projectiles.Remove(projectile);
    }

    public List<Item> Get()
    {
        return _projectiles;
    }
}