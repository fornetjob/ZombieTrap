using Assets.Scripts.Features.Prefabs;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsPooling : IDependency
{
    #region Services

    private ResourceService _resourceService = null;

    #endregion

    #region Fields

    private Queue<ViewComposite>
        _destroyed = new Queue<ViewComposite>();

    #endregion

    public void Create(string path, GameEntity entity)
    {
        ViewComposite item;

        if (_destroyed.Count > 0)
        {
            item = _destroyed.Dequeue();

            item.Root.SetActive(true);
        }
        else
        {
            var root = GameObject.Instantiate(_resourceService.GetPrefab(path));

            var views = root.GetComponentsInChildren<ViewBase>(true);

            item = new ViewComposite(root, views); 
        }

        entity.AddAttached(item);

        item.AttachEntity(entity);
    }

    public void Destroy(ViewComposite item, GameEntity entity)
    {
        item.DettachEntity(entity);

        item.Root.gameObject.SetActive(false);

        _destroyed.Enqueue(item);
    }
}