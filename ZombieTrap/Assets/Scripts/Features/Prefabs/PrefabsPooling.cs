using System.Collections.Generic;
using UnityEngine;

public class PrefabsPooling : IDependency
{
    #region Services

    private ResourceService _resourceService = null;

    #endregion

    #region Fields

    private Queue<GameObject>
        _destroyed = new Queue<GameObject>();

    #endregion

    public GameObject Create(string path, GameEntity entity)
    {
        GameObject item;

        if (_destroyed.Count > 0)
        {
            item = _destroyed.Dequeue();

            item.gameObject.SetActive(true);
        }
        else
        {
            item = GameObject.Instantiate(_resourceService.GetPrefab(path));
        }

        var views = item.GetComponentsInChildren<ViewBase>(true);

        for (int i = 0; i < views.Length; i++)
        {
            views[i].AttachEntity(entity);
        }

        return item;
    }
}