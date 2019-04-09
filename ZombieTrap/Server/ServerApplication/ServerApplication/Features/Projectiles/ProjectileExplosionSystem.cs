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
                        var distTo = Math.Abs(Vector2Float.Distance(projectile.Pos, item.Pos) - item.Radius);

                        if (distTo < projectile.Radius)
                        {
                            if (damagedItems == null)
                            {
                                damagedItems = new List<Item>();
                            }

                            var damage = (int)(projectile.Health * (distTo / projectile.Radius));

                            item.Health = Math.Max(0, item.Health - damage);
                            item.WaitTo = _timeService.GetWaitTime(3f);
                            item.State = ItemState.Wait;

                            if (item.Health == 0)
                            {
                                _itemsPooling.Remove(item);
                            }

                            damagedItems.Add(item);
                        }
                    }
                }

                if (damagedItems != null)
                {
                    _messageFactory.CreateDamagedMessage(projectile.RoomId, projectile.Pos, damagedItems);
                }

                _projectilePooling.Remove(projectile);

                projectileIndex--;
            }
        }
    }
}