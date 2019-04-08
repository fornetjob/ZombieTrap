using Game.Core;
using UnityEngine;

public class ResourceService : IService
{
    private WeakDictionary<string, GameObject>
        _prefabs = new WeakDictionary<string, GameObject>((path) => Resources.Load<GameObject>(path));

    public GameObject GetPrefab(string path)
    {
        return _prefabs[string.Format("Prefabs/{0}", path)];
    }
}