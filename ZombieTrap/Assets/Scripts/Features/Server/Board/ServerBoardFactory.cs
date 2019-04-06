using Assets.Scripts.Features.Core.Sprites;
using UnityEngine;

public class ServerBoardFactory : BoardFactoryBase
{
    private ResourceService _resourceService = null;

    protected override void OnCreate(GameEntity entity)
    {
        entity.AddSprite(_resourceService.GetSprite("Server/Textures/field"));
        entity.AddSpriteSize(entity.bound.value.size);

        var boardObj = new GameObject("Board");

        var view = boardObj.AddComponent<SpriteView>();
        view.AttachEntity(entity);
    }
}