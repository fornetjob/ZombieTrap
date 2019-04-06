using Assets.Scripts.Features.Core.Position;
using Assets.Scripts.Features.Core.Sprites;
using Assets.Scripts.Features.Core.Zombies;
using UnityEngine;

public class ServerZombieFactory : ZombieFactoryBase
{
    private ResourceService _resourceService = null;

    protected override void OnCreate(GameEntity entity)
    {
        entity.AddSprite(_resourceService.GetSprite("Server/Textures/zombie"));
        entity.AddSpriteSize(Vector2.one * entity.zombie.radius * 2);

        var boardObj = new GameObject("Zombie");

        var posView = boardObj.AddComponent<PositionView>();
        posView.AttachEntity(entity);

        var spriteView = boardObj.AddComponent<SpriteView>();
        spriteView.AttachEntity(entity);
    }
}