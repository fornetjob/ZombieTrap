using UnityEngine;

public class ViewBase:MonoBehaviour
{
    public void AttachEntity(GameEntity entity)
    {
        OnEntityAttach(entity);
    }

    protected virtual void OnEntityAttach(GameEntity entity)
    {

    }
}