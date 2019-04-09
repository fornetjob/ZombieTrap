using UnityEngine;

public class ViewBase:MonoBehaviour, IEntityAttach
{
    public void AttachEntity(GameEntity entity)
    {
        OnEntityAttach(entity);
    }

    public void DettachEntity(GameEntity entity)
    {
        OnEntityDettach(entity);
    }

    protected virtual void OnEntityAttach(GameEntity entity)
    {

    }

    protected virtual void OnEntityDettach(GameEntity entity)
    {

    }
}