using Game.Core;
using ServerApplication.Features.Items;
using System;
using System.Collections.Generic;

public class ProjectileExplosionSystem : IFixedExecuteSystem
{
    #region Services

    private TimeService _timeService = null;

    #endregion

    #region Poolings

    private MessageFactory _messageFactory = null;
    private ProjectilePooling _projectilePooling = null;
    private ItemsPooling _itemsPooling = null;

    #endregion

    public void FixedExecute()
    {
        var projectiles = _projectilePooling.Get();

        for (int projectileIndex = 0; projectileIndex < projectiles.Count; projectileIndex++)
        {
            var projectile = projectiles[projectileIndex];

            if (projectile.WaitTo < _timeService.GetGameTime())
            {
                var items = _itemsPooling.Get(projectile.RoomId);

                List<Item> damagedItems = null;

                for (int i = 0; i < items.Count; i++)
                {
                    var item = items[i];

                    if (item.Type.IsDamagable())
                    {
                        var distBetweenZombies = Vector2Float.Distance(projectile.Pos, item.Pos);

                        if (distBetweenZombies < projectile.Radius + item.Radius)
                        {
                            if (damagedItems == null)
                            {
                                damagedItems = new List<Item>();
                            }

                            item.Health = Math.Max(0, item.Health - projectile.Health);

                            damagedItems.Add(item);
                        }
                    }
                }

                if (damagedItems != null)
                {
                    _messageFactory.CreateDamagedMessage(projectile.RoomId, damagedItems);
                }
            }
        }
    }
}